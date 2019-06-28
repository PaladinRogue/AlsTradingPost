using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.ChangePassword;
using ApplicationManager.Domain.Identities.ConfirmIdentity;
using ApplicationManager.Domain.Identities.Create;
using ApplicationManager.Domain.Identities.CreatePassword;
using ApplicationManager.Domain.Identities.CreateTwoFactor;
using ApplicationManager.Domain.Identities.ForgotPassword;
using ApplicationManager.Domain.Identities.RegisterPassword;
using ApplicationManager.Domain.Identities.ResetPassword;
using ApplicationManager.Domain.Identities.ValidateToken;
using Common.Domain.Aggregates;
using Common.Domain.DataProtection;
using Common.Domain.Entities;
using Common.Resources;

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
            PasswordIdentity passwordIdentity = AuthenticationIdentities.OfType<PasswordIdentity>().SingleOrDefault();

            if (passwordIdentity == null)
            {
                throw new PasswordNotSetDomainException();
            }

            TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity =
                AuthenticationIdentities.OfType<TwoFactorAuthenticationIdentity>().SingleOrDefault(t =>
                    t.TwoFactorAuthenticationType == TwoFactorAuthenticationType.ForgotPassword);

            if (twoFactorAuthenticationIdentity == null)
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
            TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity =
                AuthenticationIdentities.OfType<TwoFactorAuthenticationIdentity>().SingleOrDefault(t =>
                    t.TwoFactorAuthenticationType == validateTokenDdto.TwoFactorAuthenticationType);

            if (twoFactorAuthenticationIdentity == null)
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
            PasswordIdentity passwordIdentity = (PasswordIdentity)AuthenticationIdentities.SingleOrDefault(a => a is PasswordIdentity);

            if (passwordIdentity == null)
            {
                throw new PasswordNotSetDomainException();
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
            PasswordIdentity passwordIdentity = (PasswordIdentity)AuthenticationIdentities.SingleOrDefault(a => a is PasswordIdentity);

            if (passwordIdentity == null)
            {
                throw new PasswordNotSetDomainException();
            }

            passwordIdentity.ChangePassword(changePasswordDdto);
        }

        internal PasswordIdentity RegisterPassword(
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            RegisterPasswordDdto registerPasswordDdto)
        {
            if (AuthenticationIdentities.Any(i => i is PasswordIdentity))
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

        internal RefreshTokenIdentity CreateRefreshToken(AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken, out string token)
        {
            RefreshTokenIdentity refreshTokenIdentity = (RefreshTokenIdentity)AuthenticationIdentities.SingleOrDefault(a => a is RefreshTokenIdentity);
            if (refreshTokenIdentity != null)
            {
                _authenticationIdentities.Remove(refreshTokenIdentity);
            }

            refreshTokenIdentity = RefreshTokenIdentity.Create(this, authenticationGrantTypeRefreshToken, out token);

            _authenticationIdentities.Add(refreshTokenIdentity);

            return refreshTokenIdentity;
        }

        internal void Logout()
        {
            Session?.Revoke();
        }
    }
}