using Common.Domain.DomainEvents.Interfaces;

namespace ApplicationManager.Domain.Identities.Events
{
    public class TwoFactorAuthenticationIdentityCreatedDomainEvent : IDomainEvent
    {
        protected TwoFactorAuthenticationIdentityCreatedDomainEvent(TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity)
        {
            TwoFactorAuthenticationIdentity = twoFactorAuthenticationIdentity;
        }

        public TwoFactorAuthenticationIdentity TwoFactorAuthenticationIdentity { get; }

        public static TwoFactorAuthenticationIdentityCreatedDomainEvent Create(TwoFactorAuthenticationIdentity twoFactorAuthenticationIdentity)
        {
            return new TwoFactorAuthenticationIdentityCreatedDomainEvent(twoFactorAuthenticationIdentity);
        }
    }
}
