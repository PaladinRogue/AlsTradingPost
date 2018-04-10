using System;
using System.Threading.Tasks;
using Common.Domain.DomainEvents.Interfaces;
using DomainEvent.Broker.Interfaces;

namespace DomainEvent.Broker
{
    public class DomainEventBus : IDomainEventBus
    {
        private readonly IDomainEventBusSubscriptionsManager _domainEventBusSubscriptionsManager;

        public DomainEventBus(IDomainEventBusSubscriptionsManager domainEventBusSubscriptionsManager)
        {
            _domainEventBusSubscriptionsManager = domainEventBusSubscriptionsManager;
        }

        public void Publish(IDomainEvent domainEvent)
        {
            Task.FromResult(ProcessDomainEvent(domainEvent.GetType().Name, domainEvent));
        }

        public void Subscribe<T, TH>(Action<T> handler) where T : IDomainEvent where TH : IDomainEventHandler<T>
        {
            _domainEventBusSubscriptionsManager.AddSubscription<T, TH>(handler);
        }

        public void Unsubscribe<T, TH>() where T : IDomainEvent where TH : IDomainEventHandler<T>
        {
            _domainEventBusSubscriptionsManager.RemoveSubscription<T, TH>();
        }

        private async Task ProcessDomainEvent(string domainEventName, IDomainEvent domainEvent)
        {
            if (_domainEventBusSubscriptionsManager.HasSubscriptionsForDomainEvent(domainEventName))
            {
                var subscriptions = _domainEventBusSubscriptionsManager.GetSubscribersForDomainEvent(domainEventName);

                await Task.Run(() => Parallel.ForEach(subscriptions,
                    subscription => { subscription.Handler.DynamicInvoke(domainEvent); }));
            }
        }

        public void Dispose()
        {
            _domainEventBusSubscriptionsManager.Clear();
        }
    }
}