using System.Threading.Tasks;
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

        public Task DispatchEventAsync<T>(T domainEvent) where T : IDomainEvent
        {
            return _domainEventBus.PublishAsync(domainEvent);
        }
    }
}
