using System;
using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.AuthenticationServices;
using Common.Domain.Models.DataProtection;

namespace ApplicationManager.Domain.Identities.AuthenticationIdentities
{
    public class PasswordIdentity : AuthenticationIdentity
    {
        public override string Type
        {
            get => AuthenticationIdentityTypes.Password;
            protected set => throw new NotSupportedException();
        }

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
            EmailAddress = createPasswordIdentityDdto.EmailAddress;
        }

        internal static PasswordIdentity Create(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            CreatePasswordIdentityDdto createPasswordIdentityDdto) 
        {
            PasswordIdentity passwordIdentity = new PasswordIdentity(identity, authenticationGrantTypePassword, createPasswordIdentityDdto);

            return passwordIdentity;
        }

        [MaxLength(255)]
        [EmailAddress]
        [Required]
        [SensitiveInformation]
        public string EmailAddress { get; protected set; }

        [MaxLength(40)]
        [SensitiveInformation]
        public string Identifier { get; protected set; }

        [SensitiveInformation]
        public string Password { get; protected set; }

        public AuthenticationGrantTypePassword AuthenticationGrantTypePassword { get; protected set; }
    }
}
