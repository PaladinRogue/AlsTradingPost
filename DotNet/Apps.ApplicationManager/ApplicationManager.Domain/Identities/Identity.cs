using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.ChangePassword;
using ApplicationManager.Domain.Identities.Create;
using ApplicationManager.Domain.Identities.CreateClientCredential;
using ApplicationManager.Domain.Identities.CreatePassword;
using ApplicationManager.Domain.Identities.CreateTwoFactor;
using ApplicationManager.Domain.Identities.ForgotPassword;
using ApplicationManager.Domain.Identities.RegisterClientCredential;
using ApplicationManager.Domain.Identities.RegisterPassword;
using ApplicationManager.Domain.Identities.ResendConfirmIdentity;
using ApplicationManager.Domain.Identities.ResetPassword;
using ApplicationManager.Domain.Identities.ValidateToken;
using Common.Domain.Aggregates;
using Common.Domain.DataProtection;
using Common.Domain.Entities;
using Common.Resources;
using ConfirmIdentityDdto = ApplicationManager.Domain.Identities.ConfirmIdentity.ConfirmIdentityDdto;

namespace ApplicationManager.Domain.Identities
{
    public class Identity : VersionedEntity, IAggregateRoot
    {
        private const byte MaskLength = 20;
        private readonly string _emailMask = new string('*', MaskLength);

        protected Identity()
        {
        }

        private Identity(CreateIdentityDdto createIdentityDdto)
        {
            EmailAddress = createIdentityDdto.EmailAddress;
        }

        private readonly ISet<AuthenticationIdentity> _authenticationIdentities = new HashSet<AuthenticationIdentity>();

        internal static Identity Create(CreateIdentityDdto createIdentityDdto)
        {
            return new Identity(createIdentityDdto);
        }

        public virtual Session Session { get; protected set; }

        // TODO only need an email address for Password identity
        public string EmailAddress
        {
            get => _emailMask;
            protected set => EmailAddressHash = DataProtection.Hash(value, StaticSalts.EmailAddress).Hash;
        }

        [Required]
        [MaxLength(FieldSizes.Protected)]
        public string EmailAddressHash { get; protected set; }

        public virtual IEnumerable<AuthenticationIdentity> AuthenticationIdentities => _authenticationIdentities;

        internal void ResetPassword(ResetPasswordDdto resetPasswordDdto)
        {
            if (!(AuthenticationIdentities.SingleOrDefault(a => a is PasswordIdentity) is PasswordIdentity passwordIdentity))
            {
                throw new PasswordNotSetDomainException();
            }

            if (!(AuthenticationIdentities.SingleOrDefault(t => t is TwoFactorAuthenticationIdentity
                && (t as TwoFactorAuthenticationIdentity).TwoFactorAuthenticationType == TwoFactorAuthenticationType.ForgotPassword)
                is TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity))
            {
                throw new InvalidTwoFactorTokenDomainException();
            }

            bool validToken = twoFactorAuthenticationIdentity.ValidateToken(new ValidateTokenDdto
            {
                Token = resetPasswordDdto.Token
            });

            if (!validToken)
            {
                throw new InvalidTwoFactorTokenDomainException();
            }

            _authenticationIdentities.Remove(twoFactorAuthenticationIdentity);

            passwordIdentity.ChangePassword(new ChangePasswordDdto
            {
                Password = resetPasswordDdto.Password
            });

            Session?.Revoke();
        }

        internal void ConfirmIdentity(ConfirmIdentityDdto confirmIdentityDdto)
        {
            _authenticationIdentities.Remove(ValidateToken(new ValidateTokenDdto
            {
                Token = confirmIdentityDdto.Token,
                TwoFactorAuthenticationType = TwoFactorAuthenticationType.ConfirmIdentity
            }));
        }

        internal TwoFactorAuthenticationIdentity ValidateToken(ValidateTokenDdto validateTokenDdto)
        {
            if (!(AuthenticationIdentities.SingleOrDefault(t => t is TwoFactorAuthenticationIdentity
                && (t as TwoFactorAuthenticationIdentity).TwoFactorAuthenticationType == validateTokenDdto.TwoFactorAuthenticationType)
                is TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity))
            {
                throw new InvalidTwoFactorTokenDomainException();
            }

            bool validToken = twoFactorAuthenticationIdentity.ValidateToken(validateTokenDdto);

            if (!validToken)
            {
                throw new InvalidTwoFactorTokenDomainException();
            }

            return twoFactorAuthenticationIdentity;
        }

        internal void ForgotPassword(ForgotPasswordDdto forgotPasswordDdto)
        {
            if (!(AuthenticationIdentities.SingleOrDefault(a => a is PasswordIdentity) is PasswordIdentity))
            {
                throw new PasswordNotSetDomainException();
            }

            if (AuthenticationIdentities.SingleOrDefault(t => t is TwoFactorAuthenticationIdentity
                && (t as TwoFactorAuthenticationIdentity).TwoFactorAuthenticationType == TwoFactorAuthenticationType.ForgotPassword)
                is TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity)
            {
                _authenticationIdentities.Remove(twoFactorAuthenticationIdentity);
            }

            _authenticationIdentities.Add(TwoFactorAuthenticationIdentity.Create(this, new CreateTwoFactorAuthenticationIdentityDdto
            {
                EmailAddress = forgotPasswordDdto.EmailAddress,
                TwoFactorAuthenticationType = TwoFactorAuthenticationType.ForgotPassword
            }));
        }

        internal void Login()
        {
            Session = Session ?? Session.Create(this);

            Session.Reinstate();
        }

        internal void ChangePassword(ChangePasswordDdto changePasswordDdto)
        {
            if (!(AuthenticationIdentities.SingleOrDefault(a => a is PasswordIdentity) is PasswordIdentity passwordIdentity))
            {
                throw new PasswordNotSetDomainException();
            }

            passwordIdentity.ChangePassword(changePasswordDdto);
        }

        internal PasswordIdentity RegisterPassword(
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            RegisterPasswordDdto registerPasswordDdto)
        {
            if (_authenticationIdentities.Any(i => i is PasswordIdentity))
            {
                throw new PasswordIdentityExistsDomainException();
            }

            PasswordIdentity passwordIdentity = PasswordIdentity.Create(this, authenticationGrantTypePassword, new CreatePasswordIdentityDdto
            {
                Identifier = registerPasswordDdto.Identifier,
                Password = registerPasswordDdto.Password
            });

            _authenticationIdentities.Add(passwordIdentity);

            _authenticationIdentities.Add(TwoFactorAuthenticationIdentity.Create(this, new CreateTwoFactorAuthenticationIdentityDdto
            {
                EmailAddress = registerPasswordDdto.EmailAddress,
                TwoFactorAuthenticationType = TwoFactorAuthenticationType.ConfirmIdentity
            }));

            return passwordIdentity;
        }

        internal ClientCredentialIdentity RegisterClientCredential(
            AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            RegisterClientCredentialDdto registerClientCredentialDdto)
        {
            if (_authenticationIdentities.Any(i => i is ClientCredentialIdentity &&
                                                  (i as ClientCredentialIdentity).AuthenticationGrantTypeClientCredential == authenticationGrantTypeClientCredential))
            {
                throw new ClientCredentialIdentityExistsDomainException();
            }

            ClientCredentialIdentity clientCredentialIdentity = ClientCredentialIdentity.Create(this, authenticationGrantTypeClientCredential, new CreateClientCredentialIdentityDdto
            {
                Identifier = registerClientCredentialDdto.Identifier
            });

            _authenticationIdentities.Add(clientCredentialIdentity);

            return clientCredentialIdentity;
        }

        internal RefreshTokenIdentity CreateRefreshToken(AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken,
            out string token)
        {
            if (AuthenticationIdentities.SingleOrDefault(a => a is RefreshTokenIdentity) is RefreshTokenIdentity refreshTokenIdentity)
            {
                _authenticationIdentities.Remove(refreshTokenIdentity);
            }

            refreshTokenIdentity = RefreshTokenIdentity.Create(this, authenticationGrantTypeRefreshToken, out token);

            _authenticationIdentities.Add(refreshTokenIdentity);

            return refreshTokenIdentity;
        }

        internal void ResendConfirmIdentity()
        {
            if (!(AuthenticationIdentities.SingleOrDefault(a =>
                a is TwoFactorAuthenticationIdentity
                && (a as TwoFactorAuthenticationIdentity).TwoFactorAuthenticationType == TwoFactorAuthenticationType.ConfirmIdentity)
                is TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity))
            {
                throw new IdentityAlreadyConfirmedDomainException();
            }

            _authenticationIdentities.Remove(twoFactorAuthenticationIdentity);

            _authenticationIdentities.Add(TwoFactorAuthenticationIdentity.Create(this, new CreateTwoFactorAuthenticationIdentityDdto
            {
                EmailAddress = twoFactorAuthenticationIdentity.EmailAddress,
                TwoFactorAuthenticationType = TwoFactorAuthenticationType.ConfirmIdentity
            }));
        }

        internal void Logout()
        {
            Session?.Revoke();
        }
    }
}