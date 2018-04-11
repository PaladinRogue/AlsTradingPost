using System;
using System.Collections.Generic;
using Common.Domain.DomainEvents.Interfaces;
using Common.Resources;

namespace DomainEvent.Broker.Interfaces
{
    public interface IDomainEventBusSubscriptionsManager
    {
        event EventHandler<string> OnDomainEventRemoved;

        void AddSubscription<T, TH>(Action<T> handler)
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>;

        void RemoveSubscription<T, TH>()
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>;

        void Clear();

        IEnumerable<Subscription> GetSubscribersForDomainEvent(Type domainEventType, bool includeInterfaces = false);
    }
}
