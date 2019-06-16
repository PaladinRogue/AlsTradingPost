using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.AddConfirmedPassword;
using Common.Domain.Models.DataProtection;

namespace ApplicationManager.Domain.Identities
{
    public class PasswordIdentity : AuthenticationIdentity
    {
        private const byte MaskLength = 8;

        protected PasswordIdentity()
        {
        }

        protected PasswordIdentity(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            AddConfirmedPasswordIdentityDdto addConfirmedPasswordIdentityDdto)
        {
            Identity = identity;
            AuthenticationGrantTypePassword = authenticationGrantTypePassword;
            Identifier = addConfirmedPasswordIdentityDdto.Identifier;
            Password = addConfirmedPasswordIdentityDdto.Password;
        }

        internal static PasswordIdentity Create(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            AddConfirmedPasswordIdentityDdto addConfirmedPasswordIdentityDdto)
        {
            return new PasswordIdentity(identity, authenticationGrantTypePassword, addConfirmedPasswordIdentityDdto);
        }

        [MaxLength(254)]
        [Required]
        [SensitiveInformation]
        public string Identifier { get; protected set; }

        [MaxLength(40)]
        [SensitiveInformation]
        public string Password { get; protected set; }

        public string PasswordMask { get; } = new string('*', MaskLength);

        public virtual AuthenticationGrantTypePassword AuthenticationGrantTypePassword { get; protected set; }
    }
}
