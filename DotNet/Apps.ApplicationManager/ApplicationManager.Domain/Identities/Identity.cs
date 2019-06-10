using System.Collections.Generic;
using System.Linq;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.AuthenticationIdentities;
using ApplicationManager.Domain.Identities.Events;
using ApplicationManager.Domain.Identities.Sessions;
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
        /// <exception cref="PasswordIdentityExistsDomainException"></exception>
        /// <param name="authenticationGrantTypePassword"></param>
        /// <param name="createPasswordIdentityDdto"></param>
        internal void CreatePasswordIdentity(
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            CreatePasswordIdentityDdto createPasswordIdentityDdto)
        {
            if (AuthenticationIdentities.Any(i => i is PasswordIdentity))
            {
                throw new PasswordIdentityExistsDomainException();
            }

            _authenticationIdentities.Add(PasswordIdentity.Create(this, authenticationGrantTypePassword, createPasswordIdentityDdto));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="TwoFactorAuthenticationIdentityExistsDomainException"></exception>
        /// <param name="createTwoFactorAuthenticationIdentityDdto"></param>
        internal void CreateTwoFactorAuthenticationIdentity(
            CreateTwoFactorAuthenticationIdentityDdto createTwoFactorAuthenticationIdentityDdto)
        {
            if (AuthenticationIdentities.Any(i => i is TwoFactorAuthenticationIdentity))
            {
                throw new TwoFactorAuthenticationIdentityExistsDomainException();
            }

            TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity =
                TwoFactorAuthenticationIdentity.Create(this, createTwoFactorAuthenticationIdentityDdto);

            _authenticationIdentities.Add(twoFactorAuthenticationIdentity);

            DomainEvents.Raise(TwoFactorAuthenticationIdentityCreatedDomainEvent.Create(twoFactorAuthenticationIdentity));
        }

        internal bool Validate(ValidatePasswordIdentityDdto validatePasswordIdentityDdto)
        {
            return _authenticationIdentities.Any(p =>
                p is PasswordIdentity identity &&
                identity.Identifier == validatePasswordIdentityDdto.Identifier &&
                identity.Password == DataProtection.Protect(validatePasswordIdentityDdto.Password));
        }
    }
}
