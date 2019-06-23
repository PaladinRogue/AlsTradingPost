using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.Identities.CreateTwoFactor;
using ApplicationManager.Domain.Identities.Events;
using Common.Domain.DomainEvents;
using Common.Domain.Models.DataProtection;
using String = Common.Resources.Extensions.String;

namespace ApplicationManager.Domain.Identities
{
    public class TwoFactorAuthenticationIdentity : AuthenticationIdentity
    {
        protected TwoFactorAuthenticationIdentity()
        {
        }

        internal TwoFactorAuthenticationIdentity(
            Identity identity,
            CreateTwoFactorAuthenticationIdentityDdto createTwoFactorAuthenticationIdentityDdto)
        {
            Identity = identity;
            EmailAddress = createTwoFactorAuthenticationIdentityDdto.EmailAddress;
            TwoFactorAuthenticationType = createTwoFactorAuthenticationIdentityDdto.TwoFactorAuthenticationType;
            Token = String.Random(40);
        }

        internal static TwoFactorAuthenticationIdentity Create(
            Identity identity,
            CreateTwoFactorAuthenticationIdentityDdto createTwoFactorAuthenticationIdentityDdto)
        {
            TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity = new TwoFactorAuthenticationIdentity(identity, createTwoFactorAuthenticationIdentityDdto);

            DomainEvents.Raise(TwoFactorAuthenticationIdentityCreatedDomainEvent.Create(twoFactorAuthenticationIdentity));

            return twoFactorAuthenticationIdentity;
        }

        [MaxLength(254)]
        [EmailAddress]
        [SensitiveInformation]
        public string EmailAddress { get; protected set; }

        [MaxLength(40)]
        [SensitiveInformation]
        public string Token { get; protected set; }

        public string TwoFactorAuthenticationType { get; protected set; }
    }
}
