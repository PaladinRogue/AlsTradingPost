using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using PaladinRogue.Authentication.Domain.AuthenticationServices;
using PaladinRogue.Authentication.Domain.Identities.ChangePassword;
using PaladinRogue.Authentication.Domain.Identities.CheckPassword;
using PaladinRogue.Authentication.Domain.Identities.CreatePassword;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Domain.DataProtectors;

namespace PaladinRogue.Authentication.Domain.Identities
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
            protected set => EmailAddressHash = DataProtection.StaticHashAsync(value, DataKeys.EmailAddressSalt).Result.Hash;
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
            protected set => PasswordHash = DataProtection.HashAsync(value).Result;
        }

        [Required]
        protected virtual HashSet PasswordHash { get; set; }

        public virtual AuthenticationGrantTypePassword AuthenticationGrantTypePassword { get; protected set; }

        internal async Task<bool> CheckPassword(CheckPasswordDdto checkPasswordDdto)
        {
            return PasswordHash == await DataProtection.HashAsync(checkPasswordDdto.Password, PasswordHash.Salt);
        }

        internal async Task ChangePassword(ChangePasswordDdto changePasswordDdto)
        {
            PasswordHash = await DataProtection.HashAsync(changePasswordDdto.Password);
        }
    }
}
