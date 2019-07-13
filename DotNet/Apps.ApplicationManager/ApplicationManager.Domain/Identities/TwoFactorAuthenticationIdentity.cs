using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ApplicationManager.Domain.Identities.CreateTwoFactor;
using ApplicationManager.Domain.Identities.Events;
using ApplicationManager.Domain.Identities.ValidateToken;
using Common.Domain.Clocks;
using Common.Domain.DataProtection;
using Common.Domain.DomainEvents;
using Common.Resources;
using NodaTime;
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
            TokenExpiry = Instant.Add(Clock.Now(),Duration.FromMinutes(20));
        }

        internal static async Task<TwoFactorAuthenticationIdentity> Create(
            Identity identity,
            CreateTwoFactorAuthenticationIdentityDdto createTwoFactorAuthenticationIdentityDdto)
        {
            TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity = new TwoFactorAuthenticationIdentity(identity, createTwoFactorAuthenticationIdentityDdto);

            await DomainEvents.RaiseAsync(TwoFactorAuthenticationIdentityCreatedDomainEvent.Create(twoFactorAuthenticationIdentity));

            return twoFactorAuthenticationIdentity;
        }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        [EmailAddress]
        [SensitiveInformation]
        public string EmailAddress { get; protected set; }

        [Required]
        [MaxLength(FieldSizes.Protected)]
        public string Token { get; set; }

        [Required]
        public Instant TokenExpiry { get; protected set; }

        [Required]
        public TwoFactorAuthenticationType TwoFactorAuthenticationType { get; protected set; }

        internal bool ValidateToken(ValidateTokenDdto validateTokenDdto)
        {
            return TwoFactorAuthenticationType == validateTokenDdto.TwoFactorAuthenticationType && Token == validateTokenDdto.Token && TokenExpiry >= Clock.Now();
        }
    }
}
