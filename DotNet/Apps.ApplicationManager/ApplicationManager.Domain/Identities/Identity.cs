using System.Collections.Generic;
using System.Linq;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.AuthenticationIdentities;
using ApplicationManager.Domain.Identities.Sessions;
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

        public static Identity Create()
        {
            return new Identity();
        }

        public Session Session { get; protected set; }

        public IEnumerable<AuthenticationIdentity> AuthenticationIdentities => _authenticationIdentities;

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
            if (AuthenticationIdentities.Any(i => i.Type == AuthenticationIdentityTypes.Password))
            {
                throw new PasswordIdentityExistsDomainException();
            }

            _authenticationIdentities.Add(PasswordIdentity.Create(this, authenticationGrantTypePassword, createPasswordIdentityDdto));
        }

        public bool Validate(ValidatePasswordIdentityDdto validatePasswordIdentityDdto)
        {
            return _authenticationIdentities.Any(p =>
                p.Type == AuthenticationIdentityTypes.Password &&
                (p as PasswordIdentity)?.Identifier == validatePasswordIdentityDdto.Identifier &&
                (p as PasswordIdentity)?.Password == DataProtection.Protect(validatePasswordIdentityDdto.Password));
        }
    }
}
