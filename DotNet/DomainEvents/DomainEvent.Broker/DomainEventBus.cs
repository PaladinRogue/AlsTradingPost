using System.Collections.Generic;
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

        public void Publish<T>(T domainEvent) where T : IDomainEvent
        {
            ProcessDomainEvent(domainEvent);
        }

        private void ProcessDomainEvent<T>(T domainEvent) where T : IDomainEvent
        {
            IEnumerable<IDomainEventHandler<T>> domainEventHandlers = _domainEventHandlerResolver.ResolveAll<T>();
            foreach (IDomainEventHandler<T> domainEventHandler in domainEventHandlers)
            {
                //TODO Make async?
                domainEventHandler.Handle(domainEvent);
            }
        }
    }
}