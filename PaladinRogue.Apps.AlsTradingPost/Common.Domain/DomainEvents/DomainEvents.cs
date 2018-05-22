using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public class DomainEvents
    {
        private static volatile IDomainEventDispatcher _domainEventDispatcher;

        protected DomainEvents()
        {
        }

        protected static IDomainEventDispatcher DomainEventDispatcher
        {
            get => _domainEventDispatcher;
            set => _domainEventDispatcher = value;
        }

        public static void SetDomainEventDispatcher(IDomainEventDispatcher domainEventDispatcher)
        {
            if (DomainEventDispatcher == null)
            {
                DomainEventDispatcher = domainEventDispatcher;
            }
        }

        public static void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            if (DomainEventDispatcher == null)
            {
                throw new DomainEventDispatcherNotSetException();
            }

            DomainEventDispatcher.DispatchEvent(domainEvent);
        }
    }
}