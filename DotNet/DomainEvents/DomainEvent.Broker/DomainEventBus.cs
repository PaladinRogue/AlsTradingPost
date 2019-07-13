using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Domain.DomainEvents.Interfaces;

namespace DomainEvent.Broker
{
    public class DomainEventBus : IDomainEventBus
    {
        private readonly IDomainEventHandlerResolver _domainEventHandlerResolver;

        public DomainEventBus(IDomainEventHandlerResolver domainEventHandlerResolver)
        {
            _domainEventHandlerResolver = domainEventHandlerResolver;
        }

        public Task PublishAsync<T>(T domainEvent) where T : IDomainEvent
        {
            return ProcessDomainEventAsync(domainEvent);
        }

        private async Task ProcessDomainEventAsync<T>(T domainEvent) where T : IDomainEvent
        {
            IEnumerable<IDomainEventHandler<T>> domainEventHandlers = _domainEventHandlerResolver.ResolveAll<T>();
            foreach (IDomainEventHandler<T> domainEventHandler in domainEventHandlers)
            {
                await domainEventHandler.HandleAsync(domainEvent);
            }
        }
    }
}