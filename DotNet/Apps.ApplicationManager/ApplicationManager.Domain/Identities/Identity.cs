using System.Collections.Generic;
using ApplicationManager.Domain.AuthenticationServices.Identities;
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

        public static Identity Create()
        {
            return new Identity();
        }

        public Session Session { get; protected set; }

        public IEnumerable<PasswordIdentity> PasswordIdentities => _passwordIdentities;

        public PasswordIdentity Create(
            Identity identity,
            CreatePasswordIdentityDdto createPasswordIdentityDdto)
        {
            return PasswordIdentity.Create(identity, this, createPasswordIdentityDdto);
        }

        public bool Validate(ValidatePasswordIdentityDdto validatePasswordIdentityDdto)
        {
            return _passwordIdentities.Any(p =>
                p.Identifier == validatePasswordIdentityDdto.Identifier &&
                p.Password == DataProtection.Protect(validatePasswordIdentityDdto.Password));
        }
    }
}
