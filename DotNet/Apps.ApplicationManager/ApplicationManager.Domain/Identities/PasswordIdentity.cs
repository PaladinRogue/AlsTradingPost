using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.ChangePassword;
using ApplicationManager.Domain.Identities.CheckPassword;
using ApplicationManager.Domain.Identities.CreatePassword;
using Common.Domain.DataProtection;
using Common.Resources;

namespace ApplicationManager.Domain.Identities
{
    public class PasswordIdentity : AuthenticationIdentity
    {
        private const byte EmailMaskLength = 20;
        private readonly string _emailMask = new string('*', EmailMaskLength);

        private const byte PasswordMaskLength = 8;
        private readonly string _passwordMask = new string('*', PasswordMaskLength);

        protected PasswordIdentity()
        {
        }

        private PasswordIdentity(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            CreatePasswordIdentityDdto createPasswordIdentityDdto)
        {
            Identity = identity;
            AuthenticationGrantTypePassword = authenticationGrantTypePassword;
            Identifier = createPasswordIdentityDdto.Identifier;
            Password = createPasswordIdentityDdto.Password;
            EmailAddress = createPasswordIdentityDdto.EmailAddress;
        }

        internal static PasswordIdentity Create(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            CreatePasswordIdentityDdto createPasswordIdentityDdto)
        {
            return new PasswordIdentity(identity, authenticationGrantTypePassword, createPasswordIdentityDdto);
        }

        public string EmailAddress
        {
            get => _emailMask;
            protected set => EmailAddressHash = DataProtection.Hash(value, StaticSalts.EmailAddress).Hash;
        }

        [Required]
        [MaxLength(FieldSizes.Protected)]
        public string EmailAddressHash { get; protected set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string Identifier { get; protected set; }

        public string Password
        {
            get => _passwordMask;
            protected set => PasswordHash = DataProtection.Hash(value);
        }

        [Required]
        protected virtual HashSet PasswordHash { get; set; }

        public virtual AuthenticationGrantTypePassword AuthenticationGrantTypePassword { get; protected set; }

        internal bool CheckPassword(CheckPasswordDdto checkPasswordDdto)
        {
            return PasswordHash == DataProtection.Hash(checkPasswordDdto.Password, PasswordHash.Salt);
        }

        internal void ChangePassword(ChangePasswordDdto changePasswordDdto)
        {
            PasswordHash = DataProtection.Hash(changePasswordDdto.Password);
        }
    }
}
