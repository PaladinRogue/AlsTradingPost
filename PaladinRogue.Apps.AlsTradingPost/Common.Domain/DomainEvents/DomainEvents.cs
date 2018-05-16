using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public class DomainEvents
    {
        private static volatile IPendingDomainEventDirector _pendingDomainEventDirector;

        protected DomainEvents()
        {
        }

        protected static IPendingDomainEventDirector PendingDomainEventDirector
        {
            get => _pendingDomainEventDirector;
            set => _pendingDomainEventDirector = value;
        }

        public static void SetPendingDomainEventDirector(IPendingDomainEventDirector pendingDomainEventDirector)
        {
            if (PendingDomainEventDirector == null)
            {
                PendingDomainEventDirector = pendingDomainEventDirector;
            }
        }

        public static void Raise(IDomainEvent domainEvent)
        {
            if (PendingDomainEventDirector == null)
            {
                throw new PendingDomainEventDirectorNotSetException();
            }

            PendingDomainEventDirector.Add(domainEvent);
        }
    }
}