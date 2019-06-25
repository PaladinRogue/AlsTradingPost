using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.Identities.CreateTwoFactor;
using ApplicationManager.Domain.Identities.Events;
using ApplicationManager.Domain.Identities.ValidateToken;
using Common.Domain.DomainEvents;
using Common.Domain.Models.DataProtection;
using Common.Resources;
using String = Common.Resources.Extensions.String;

namespace ApplicationManager.Domain.Identities
{
    public class TwoFactorAuthenticationIdentity : AuthenticationIdentity
    {
        protected TwoFactorAuthenticationIdentity()
        {
        }

        private TwoFactorAuthenticationIdentity(
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

        [Required]
        [MaxLength(254)]
        [EmailAddress]
        [SensitiveInformation]
        public string EmailAddress { get; protected set; }

        [Required]
        [MaxLength(FieldSizes.Protected)]
        public string Token { get; set; }

        [Required]
        public TwoFactorAuthenticationType TwoFactorAuthenticationType { get; protected set; }

        internal bool ValidateToken(ValidateTokenDdto validateTokenDdto)
        {
            return Token == validateTokenDdto.Token;
        }
    }
}
