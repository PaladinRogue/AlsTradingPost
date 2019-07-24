using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Authentication.Domain.Identities.CreateTwoFactor;
using Authentication.Domain.Identities.Events;
using Authentication.Domain.Identities.ValidateToken;
using Common.Domain.Clocks;
using Common.Domain.DataProtectors;
using Common.Domain.DomainEvents;
using Common.Resources;
using NodaTime;
using String = Common.Resources.Extensions.String;

namespace Authentication.Domain.Identities
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
        [SensitiveInformation(DataKeys.EmailAddress)]
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
