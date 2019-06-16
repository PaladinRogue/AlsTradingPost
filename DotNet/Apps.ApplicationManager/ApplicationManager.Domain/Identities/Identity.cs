using System.Collections.Generic;
using System.Linq;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.AddConfirmedPassword;
using ApplicationManager.Domain.Identities.AddTwoFactor;
using ApplicationManager.Domain.Identities.Events;
using ApplicationManager.Domain.Identities.ValidatePassword;
using ApplicationManager.Domain.Identities.ValidateTwoFactor;
using Common.Domain.DomainEvents;
using Common.Domain.Models;
using Common.Domain.Models.DataProtection;
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

            PasswordIdentity passwordIdentity = PasswordIdentity.Create(this, authenticationGrantTypePassword, addConfirmedPasswordIdentityDdto);

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

            TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity = TwoFactorAuthenticationIdentity.Create(this, addTwoFactorAuthenticationIdentityDdto);

            _authenticationIdentities.Add(twoFactorAuthenticationIdentity);

            DomainEvents.Raise(TwoFactorAuthenticationIdentityCreatedDomainEvent.Create(twoFactorAuthenticationIdentity));
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="validatePasswordIdentityDdto"></param>
        /// <exception cref="InvalidPasswordDomainException"></exception>
        internal void ValidatePassword(ValidatePasswordIdentityDdto validatePasswordIdentityDdto)
        {
            bool validTPassword = _authenticationIdentities.Any(p =>
                p is PasswordIdentity identity &&
                identity.Identifier == validatePasswordIdentityDdto.Identifier &&
                identity.Password == DataProtection.Protect(validatePasswordIdentityDdto.Password));

            if (!validTPassword)
            {
                throw new InvalidPasswordDomainException();
            }
        }
    }
}