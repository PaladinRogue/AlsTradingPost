using System;
using Common.Domain.DomainEvents.Interfaces;
using DomainEvent.Broker.Interfaces;

namespace DomainEvent.Broker
{
    public class DomainEventBus : IDomainEventBus
    {
        private readonly IDomainEventBusSubscriptionsManager _domainEventBusSubscriptionsManager;
        private readonly IDomainEventHandlerResolver _domainEventHandlerResolver;

        public DomainEventBus(IDomainEventBusSubscriptionsManager domainEventBusSubscriptionsManager,
            IDomainEventHandlerResolver domainEventHandlerResolver)
        {
            _domainEventBusSubscriptionsManager = domainEventBusSubscriptionsManager;
            _domainEventHandlerResolver = domainEventHandlerResolver;
        }

        public void Publish(IDomainEvent domainEvent)
        {
            ProcessDomainEvent(domainEvent.GetType(), domainEvent);
        }

        public void Subscribe<T, TH>() 
            where T : IDomainEvent 
            where TH : IDomainEventHandler<T>
        {
            _domainEventBusSubscriptionsManager.AddSubscription<T, TH>();
        }

        public void Unsubscribe<T, TH>() 
            where T : IDomainEvent 
            where TH : IDomainEventHandler<T>
        {
            _domainEventBusSubscriptionsManager.RemoveSubscription<T, TH>();
        }

        private void ProcessDomainEvent<T>(Type domainEventType, T domainEvent) where T : IDomainEvent
        {
            foreach (DomainEventSubscription subscription in _domainEventBusSubscriptionsManager.GetSubscribersForDomainEvent(domainEventType, domainEventType.IsClass))
            {
                IDomainEventHandler<IDomainEvent> domainEventHandler = _domainEventHandlerResolver.Resolve(subscription.HandlerType);
                domainEventHandler.Handle(domainEvent);
            }
        }

        public void Dispose()
        {
            _domainEventBusSubscriptionsManager.Clear();
        }
    }
}