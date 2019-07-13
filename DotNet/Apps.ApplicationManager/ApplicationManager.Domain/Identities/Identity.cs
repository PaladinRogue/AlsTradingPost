using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.ChangePassword;
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
using Common.Domain.Entities;
using ConfirmIdentityDdto = ApplicationManager.Domain.Identities.ConfirmIdentity.ConfirmIdentityDdto;

namespace ApplicationManager.Domain.Identities
{
    public class Identity : VersionedEntity, IAggregateRoot
    {
        protected Identity()
        {
        }

        private readonly ISet<AuthenticationIdentity> _authenticationIdentities = new HashSet<AuthenticationIdentity>();

        internal static Identity Create()
        {
            return new Identity();
        }

        public virtual Session Session { get; protected set; }

        public virtual IEnumerable<AuthenticationIdentity> AuthenticationIdentities => _authenticationIdentities;

        internal void ResetPassword(ResetPasswordDdto resetPasswordDdto)
        {
            if (!(AuthenticationIdentities.OfType<PasswordIdentity>().SingleOrDefault() is PasswordIdentity passwordIdentity))
            {
                throw new PasswordNotSetDomainException();
            }

            if (!(AuthenticationIdentities.OfType<TwoFactorAuthenticationIdentity>().SingleOrDefault(t => t.TwoFactorAuthenticationType == TwoFactorAuthenticationType.ForgotPassword)
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
            if (!(AuthenticationIdentities.OfType<TwoFactorAuthenticationIdentity>().SingleOrDefault(t => t.TwoFactorAuthenticationType == validateTokenDdto.TwoFactorAuthenticationType)
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

        internal async Task ForgotPassword(ForgotPasswordDdto forgotPasswordDdto)
        {
            if (AuthenticationIdentities.OfType<PasswordIdentity>().SingleOrDefault() == null)
            {
                throw new PasswordNotSetDomainException();
            }

            if (AuthenticationIdentities.OfType<TwoFactorAuthenticationIdentity>().SingleOrDefault(t => t.TwoFactorAuthenticationType == TwoFactorAuthenticationType.ForgotPassword)
                is TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity)
            {
                _authenticationIdentities.Remove(twoFactorAuthenticationIdentity);
            }

            _authenticationIdentities.Add(await TwoFactorAuthenticationIdentity.Create(this, new CreateTwoFactorAuthenticationIdentityDdto
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
            if (!(AuthenticationIdentities.OfType<PasswordIdentity>().SingleOrDefault() is PasswordIdentity passwordIdentity))
            {
                throw new PasswordNotSetDomainException();
            }

            passwordIdentity.ChangePassword(changePasswordDdto);
        }

        internal async Task<PasswordIdentity> RegisterPassword(
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            RegisterPasswordDdto registerPasswordDdto)
        {
            if (_authenticationIdentities.OfType<PasswordIdentity>().Any())
            {
                throw new PasswordIdentityExistsDomainException();
            }

            PasswordIdentity passwordIdentity = PasswordIdentity.Create(this, authenticationGrantTypePassword, new CreatePasswordIdentityDdto
            {
                Identifier = registerPasswordDdto.Identifier,
                Password = registerPasswordDdto.Password,
                EmailAddress = registerPasswordDdto.EmailAddress
            });

            _authenticationIdentities.Add(passwordIdentity);

            _authenticationIdentities.Add(await TwoFactorAuthenticationIdentity.Create(this, new CreateTwoFactorAuthenticationIdentityDdto
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
            if (AuthenticationIdentities.OfType<ClientCredentialIdentity>().Any(i => i.AuthenticationGrantTypeClientCredential == authenticationGrantTypeClientCredential))
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

        internal RefreshToken CreateRefreshToken(AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken,
            out string token)
        {
            return Session.CreateRefreshToken(authenticationGrantTypeRefreshToken, out token);
        }

        internal async Task ResendConfirmIdentity()
        {
            if (!(AuthenticationIdentities.OfType<TwoFactorAuthenticationIdentity>().SingleOrDefault(a =>
                    a.TwoFactorAuthenticationType == TwoFactorAuthenticationType.ConfirmIdentity)
                is TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity))
            {
                throw new IdentityAlreadyConfirmedDomainException();
            }

            _authenticationIdentities.Remove(twoFactorAuthenticationIdentity);

            _authenticationIdentities.Add( await TwoFactorAuthenticationIdentity.Create(this, new CreateTwoFactorAuthenticationIdentityDdto
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