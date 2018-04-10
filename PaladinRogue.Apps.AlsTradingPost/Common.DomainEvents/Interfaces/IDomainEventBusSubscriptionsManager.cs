using System;
using System.Collections.Generic;
using Common.Domain.DomainEvents.Interfaces;
using Common.Resources;

namespace DomainEvent.Broker.Interfaces
{
    public interface IDomainEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnDomainEventRemoved;

        void AddSubscription<T, TH>(Action<T> handler)
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>;

        void RemoveSubscription<T, TH>()
            where T : IDomainEvent
            where TH : IDomainEventHandler<T>;

        bool HasSubscriptionsForDomainEvent<T>() where T : IDomainEvent;

        bool HasSubscriptionsForDomainEvent(string domainEventName);

        void Clear();

        IEnumerable<Subscription> GetSubscribersForDomainEvent<T>() where T : IDomainEvent;

        IEnumerable<Subscription> GetSubscribersForDomainEvent(string DomainEventName);

        string GetDomainEventKey<T>();
    }
}
