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

        public void DispatchEvent(IDomainEvent domainEvent)
        {
            _domainEventBus.Publish(domainEvent);
        }
    }
}
