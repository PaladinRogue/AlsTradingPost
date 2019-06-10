using System;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Models.DataProtection;
using String = Common.Resources.Extensions.String;

namespace ApplicationManager.Domain.Identities.AuthenticationIdentities
{
    public class TwoFactorAuthenticationIdentity : AuthenticationIdentity
    {
        protected TwoFactorAuthenticationIdentity()
        {
        }

        protected TwoFactorAuthenticationIdentity(
            Identity identity,
            CreateTwoFactorAuthenticationIdentityDdto createTwoFactorAuthenticationIdentityDdto)
        {
            Identity = identity;
            EmailAddress = createTwoFactorAuthenticationIdentityDdto.EmailAddress;
            Token = String.Random(40);
        }

        internal static TwoFactorAuthenticationIdentity Create(
            Identity identity,
            CreateTwoFactorAuthenticationIdentityDdto createTwoFactorAuthenticationIdentityDdto)
        {
            return new TwoFactorAuthenticationIdentity(identity, createTwoFactorAuthenticationIdentityDdto);
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
