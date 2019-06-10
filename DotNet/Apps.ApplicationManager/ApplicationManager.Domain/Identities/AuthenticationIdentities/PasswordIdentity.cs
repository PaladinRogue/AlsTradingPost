using System;
using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.AuthenticationServices;
using Common.Domain.Models.DataProtection;

namespace ApplicationManager.Domain.Identities.AuthenticationIdentities
{
    public class PasswordIdentity : AuthenticationIdentity
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
            Password = createPasswordIdentityDdto.Password;
        }

        internal static PasswordIdentity Create(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            CreatePasswordIdentityDdto createPasswordIdentityDdto) 
        {
            return new PasswordIdentity(identity, authenticationGrantTypePassword, createPasswordIdentityDdto);
        }

        [MaxLength(254)]
        [Required]
        [SensitiveInformation]
        public string Identifier { get; protected set; }

        [MaxLength(40)]
        [SensitiveInformation]
        public string Password { get; protected set; }

        public virtual AuthenticationGrantTypePassword AuthenticationGrantTypePassword { get; protected set; }
    }
}
