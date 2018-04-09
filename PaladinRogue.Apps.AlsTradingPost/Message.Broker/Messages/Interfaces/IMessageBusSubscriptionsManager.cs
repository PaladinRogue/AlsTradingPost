﻿using System;
using System.Collections.Generic;
using Common.Messaging.Message.Interfaces;

namespace Message.Broker.Messages.Interfaces
{
    public interface IMessageBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnMessageRemoved;

        void AddSubscription<T, TH>(Action<T> handler)
            where T : IMessage
            where TH : IMessageSubscriber<T>;

        void RemoveSubscription<T, TH>()
            where T : IMessage
            where TH : IMessageSubscriber<T>;

        bool HasSubscriptionsForMessage<T>() where T : IMessage;

        bool HasSubscriptionsForMessage(string messageName);

        void Clear();

        IEnumerable<Subscription> GetSubscribersForMessage<T>() where T : IMessage;

        IEnumerable<Subscription> GetSubscribersForMessage(string messageName);

        string GetMessageKey<T>();
    }
}
