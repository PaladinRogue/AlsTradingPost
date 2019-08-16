using System.Threading.Tasks;
using PaladinRogue.Library.Core.Domain.DomainEvents.Interfaces;

namespace PaladinRogue.Library.Core.Domain.DomainEvents
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
