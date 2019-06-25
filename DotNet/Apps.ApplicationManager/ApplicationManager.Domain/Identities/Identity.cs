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
using Common.Domain.Models;
using Common.Domain.Models.DataProtection;
using Common.Domain.Models.Interfaces;
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
        public string EmailAddressHash { get; set; }

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
        }

        internal void ConfirmIdentity(ConfirmIdentityDdto confirmIdentityDdto)
        {
            TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity =
                AuthenticationIdentities.OfType<TwoFactorAuthenticationIdentity>().SingleOrDefault(t =>
                    t.TwoFactorAuthenticationType == TwoFactorAuthenticationType.ConfirmIdentity);

            if (twoFactorAuthenticationIdentity == null)
            {
                throw new InvalidTwoFactorTokenDomainException();
            }

            bool validToken = twoFactorAuthenticationIdentity.ValidateToken(new ValidateTokenDdto
            {
                Token = confirmIdentityDdto.Token
            });

            if (!validToken)
            {
                throw new InvalidTwoFactorTokenDomainException();
            }

            _authenticationIdentities.Remove(twoFactorAuthenticationIdentity);
        }

        internal void ForgotPassword(ForgotPasswordDdto forgotPasswordDdto)
        {
            PasswordIdentity passwordIdentity = AuthenticationIdentities.OfType<PasswordIdentity>().SingleOrDefault();

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
            PasswordIdentity passwordIdentity = AuthenticationIdentities.OfType<PasswordIdentity>().SingleOrDefault();

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

        internal RefreshTokenIdentity CreateRefreshToken(AuthenticationGrantTypeRefreshToken authenticationGrantTypeRefreshToken)
        {
            RefreshTokenIdentity refreshTokenIdentity = _authenticationIdentities.OfType<RefreshTokenIdentity>().SingleOrDefault();
            if (refreshTokenIdentity != null)
            {
                _authenticationIdentities.Remove(refreshTokenIdentity);
            }

            _authenticationIdentities.Add(RefreshTokenIdentity.Create(this, authenticationGrantTypeRefreshToken));

            return refreshTokenIdentity;
        }
    }
}