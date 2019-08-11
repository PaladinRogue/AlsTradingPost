using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Libray.Core.Domain.DomainEvents.Interfaces;

namespace PaladinRogue.Libray.DomainEvents.Broker
{
    public class DomainEventBus : IDomainEventBus
    {
        private readonly IDomainEventSubscriberResolver _domainEventSubscriberResolver;

        public DomainEventBus(IDomainEventSubscriberResolver domainEventSubscriberResolver)
        {
            _domainEventSubscriberResolver = domainEventSubscriberResolver;
        }

        public Task PublishAsync<T>(T domainEvent) where T : IDomainEvent
        {
            return ProcessDomainEventAsync(domainEvent);
        }

        private async Task ProcessDomainEventAsync<T>(T domainEvent) where T : IDomainEvent
        {
            IEnumerable<IDomainEventSubscriber<T>> domainEventSubscribers = _domainEventSubscriberResolver.ResolveAll<T>();
            foreach (IDomainEventSubscriber<T> domainEventSubscriber in domainEventSubscribers)
            {
                await domainEventSubscriber.ExecuteAsync(domainEvent);
            }
        }
    }
}