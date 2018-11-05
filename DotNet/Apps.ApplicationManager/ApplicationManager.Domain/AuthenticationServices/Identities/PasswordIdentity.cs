using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.Identities;
using Common.Domain.Models;
using Common.Domain.Models.DataProtection;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.AuthenticationServices.Identities
{
    public class PasswordIdentity : Entity, IAggregateMember
    {
        protected PasswordIdentity()
        {
        }
                                                                    
        protected PasswordIdentity(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            CreatePasswordIdentityDdto createPasswordIdentityDdto)
        {
            Identity = identity;
            AuthenticationGrantTypePassword = authenticationGrantTypePassword;
            Identifier = createPasswordIdentityDdto.Identifier;
            Password = createPasswordIdentityDdto.Password;
        }

        internal static PasswordIdentity Create(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            CreatePasswordIdentityDdto createPasswordIdentityDdto) 
        {
            return new PasswordIdentity(identity, authenticationGrantTypePassword, createPasswordIdentityDdto);
        }

        [MaxLength(40)]
        public string Identifier { get; protected set; }

        [SensitiveInformation]
        public string Password { get; protected set; }

        public Identity Identity { get; protected set; }

        public AuthenticationGrantTypePassword AuthenticationGrantTypePassword { get; protected set; }

        public IAggregateRoot AggregateRoot => AuthenticationGrantTypePassword;
    }
}
