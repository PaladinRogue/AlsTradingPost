using System;
using System.Collections.Generic;
using Common.Messaging.Interfaces;

namespace Message.Broker.Interfaces
{
    public interface IMessageBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        void AddSubscription<T, TH>(Action<T> handler)
            where T : IMessage
            where TH : IMessageSubscriber<T>;

        void RemoveSubscription<T, TH>()
            where T : IMessage
            where TH : IMessageSubscriber<T>;

        bool HasSubscriptionsForEvent<T>() where T : IMessage;

        bool HasSubscriptionsForEvent(string messageName);

        void Clear();

        IEnumerable<Subscription> GetHandlersForEvent<T>() where T : IMessage;

        IEnumerable<Subscription> GetHandlersForEvent(string messageName);

        string GetEventKey<T>();
    }
}
