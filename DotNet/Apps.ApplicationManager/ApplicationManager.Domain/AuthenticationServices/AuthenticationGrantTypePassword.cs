using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationManager.Domain.AuthenticationServices.Identities;
using ApplicationManager.Domain.Identities;
using Common.Domain.Models.DataProtection;

namespace ApplicationManager.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypePassword : AuthenticationService
    {
        public override string Discriminator
        {
            get => "PASSWORD";
            protected set => throw new NotSupportedException();
        }

        private readonly ISet<PasswordIdentity> _passwordIdentities = new HashSet<PasswordIdentity>();

        protected AuthenticationGrantTypePassword()
        {
        }

        public static AuthenticationGrantTypePassword Create()
        {
            return new AuthenticationGrantTypePassword();
        }

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
