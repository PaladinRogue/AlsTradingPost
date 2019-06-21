using System.Collections.Generic;
using System.Linq;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.AddConfirmedPassword;
using ApplicationManager.Domain.Identities.AddTwoFactor;
using ApplicationManager.Domain.Identities.ChangePassword;
using ApplicationManager.Domain.Identities.CreatePassword;
using ApplicationManager.Domain.Identities.CreateTwoFactor;
using ApplicationManager.Domain.Identities.Events;
using ApplicationManager.Domain.Identities.RegisterPassword;
using ApplicationManager.Domain.Identities.ValidateTwoFactor;
using Common.Domain.DomainEvents;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

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

        /// <summary>
        ///
        /// </summary>
        /// <param name="authenticationGrantTypePassword"></param>
        /// <param name="addConfirmedPasswordIdentityDdto"></param>
        /// <returns></returns>
        /// <exception cref="PasswordIdentityExistsDomainException"></exception>
        /// <exception cref="InvalidTwoFactorTokenDomainException"></exception>
        internal PasswordIdentity AddConfirmedPasswordIdentity(
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            AddConfirmedPasswordIdentityDdto addConfirmedPasswordIdentityDdto)
        {
            if (AuthenticationIdentities.Any(i => i is PasswordIdentity))
            {
                throw new PasswordIdentityExistsDomainException();
            }

            TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity = GetTwoFactor(
                new ValidateTwoFactorIdentityDdto
                {
                    Token = addConfirmedPasswordIdentityDdto.Token
                });

            if (twoFactorAuthenticationIdentity == null)
            {
                throw new InvalidTwoFactorTokenDomainException();
            }

            _authenticationIdentities.Remove(twoFactorAuthenticationIdentity);

            PasswordIdentity passwordIdentity = PasswordIdentity.Create(this, authenticationGrantTypePassword, new CreatePasswordIdentityDdto
            {
                Identifier = addConfirmedPasswordIdentityDdto.Identifier,
                Password = addConfirmedPasswordIdentityDdto.Password
            });

            _authenticationIdentities.Add(passwordIdentity);

            return passwordIdentity;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="addTwoFactorAuthenticationIdentityDdto"></param>
        /// <exception cref="TwoFactorAuthenticationIdentityExistsDomainException"></exception>
        internal void AddTwoFactorAuthenticationIdentity(
            AddTwoFactorAuthenticationIdentityDdto addTwoFactorAuthenticationIdentityDdto)
        {
            if (AuthenticationIdentities.Any(i => i is TwoFactorAuthenticationIdentity))
            {
                throw new TwoFactorAuthenticationIdentityExistsDomainException();
            }

            TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity = TwoFactorAuthenticationIdentity.Create(this, new CreateTwoFactorAuthenticationIdentityDdto
            {
                EmailAddress = addTwoFactorAuthenticationIdentityDdto.EmailAddress
            });

            _authenticationIdentities.Add(twoFactorAuthenticationIdentity);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="validateTwoFactorIdentityDdto"></param>
        /// <exception cref="InvalidTwoFactorTokenDomainException"></exception>
        private TwoFactorAuthenticationIdentity GetTwoFactor(
            ValidateTwoFactorIdentityDdto validateTwoFactorIdentityDdto)
        {
            return _authenticationIdentities.SingleOrDefault(p =>
                    p is TwoFactorAuthenticationIdentity identity &&
                    identity.Token == validateTwoFactorIdentityDdto.Token) as
                TwoFactorAuthenticationIdentity;
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
            PasswordIdentity passwordIdentity = PasswordIdentity.Create(this, authenticationGrantTypePassword, new CreatePasswordIdentityDdto
            {
                Identifier = registerPasswordDdto.Identifier,
                Password = registerPasswordDdto.Password
            });

            _authenticationIdentities.Add(passwordIdentity);

            _authenticationIdentities.Add(TwoFactorAuthenticationIdentity.Create(this, new CreateTwoFactorAuthenticationIdentityDdto
            {
                EmailAddress = registerPasswordDdto.EmailAddress
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