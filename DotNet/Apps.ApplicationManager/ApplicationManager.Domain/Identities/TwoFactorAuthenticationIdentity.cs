using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.Identities.Models;
using Common.Domain.Models.DataProtection;
using String = Common.Resources.Extensions.String;

namespace ApplicationManager.Domain.Identities
{
    public class TwoFactorAuthenticationIdentity : AuthenticationIdentity
    {
        protected TwoFactorAuthenticationIdentity()
        {
        }

        protected TwoFactorAuthenticationIdentity(
            Identity identity,
            AddTwoFactorAuthenticationIdentityDdto addTwoFactorAuthenticationIdentityDdto)
        {
            Identity = identity;
            EmailAddress = addTwoFactorAuthenticationIdentityDdto.EmailAddress;
            Token = String.Random(40);
        }

        internal static TwoFactorAuthenticationIdentity Create(
            Identity identity,
            AddTwoFactorAuthenticationIdentityDdto addTwoFactorAuthenticationIdentityDdto)
        {
            return new TwoFactorAuthenticationIdentity(identity, addTwoFactorAuthenticationIdentityDdto);
        }

        [MaxLength(254)]
        [EmailAddress]
        [SensitiveInformation]
        public string EmailAddress { get; protected set; }

        [MaxLength(40)]
        [SensitiveInformation]
        public string Token { get; protected set; }
    }
}
