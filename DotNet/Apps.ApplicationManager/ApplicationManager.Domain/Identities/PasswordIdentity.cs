using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.AddConfirmedPassword;
using Common.Domain.Models.DataProtection;
using Common.Domain.Models.PasswordProtection;
using Common.Resources;

namespace ApplicationManager.Domain.Identities
{
    public class PasswordIdentity : AuthenticationIdentity
    {
        private const byte MaskLength = 8;
        private readonly string _passwordMask = new string('*', MaskLength);

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

        [MaxLength(FieldSizes.Extended)]
        [Required]
        [SensitiveInformation]
        public string Identifier { get; protected set; }

        public string Password
        {
            get => _passwordMask;
            protected set => ProtectedPassword = PasswordProtection.Protect(value);
        }

        [Required]
        protected virtual ProtectedPassword ProtectedPassword { get; set; }

        public virtual AuthenticationGrantTypePassword AuthenticationGrantTypePassword { get; protected set; }

        public bool CheckPassword(string password)
        {
            return ProtectedPassword.Hash == PasswordProtection.Protect(password, ProtectedPassword.Salt).Hash;
        }
    }
}
