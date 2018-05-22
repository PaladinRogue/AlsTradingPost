using Common.Domain.DomainEvents.Interfaces;

namespace Common.Domain.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IDomainEventBus _domainEventBus;

        public DomainEventDispatcher(IDomainEventBus domainEventBus)
        {
            _domainEventBus = domainEventBus;
        }

        public void DispatchEvent<T>(T domainEvent) where T : IDomainEvent
        {
            _domainEventBus.Publish(domainEvent);
        }
    }
}
