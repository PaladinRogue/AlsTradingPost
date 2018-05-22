using System;
using System.Collections.Generic;
using Common.Domain.DomainEvents.Interfaces;

namespace DomainEvent.Broker.Interfaces
{
    public interface IDomainEventBusSubscriptionsManager
    {
        event EventHandler<string> OnDomainEventRemoved;

        void AddSubscription<T, TH>()
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>;

        void RemoveSubscription<T, TH>()
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>;

        void Clear();

        IEnumerable<DomainEventSubscription> GetSubscribersForDomainEvent(Type domainEventType, bool includeInterfaces = false);
    }
}
