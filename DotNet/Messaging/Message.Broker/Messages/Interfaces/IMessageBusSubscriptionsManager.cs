﻿using System;
using System.Collections.Generic;
using Common.Messaging.Message;
using Common.Messaging.Message.Interfaces;
using Common.Messaging.Subscribers;
using Common.Resources;

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

        IEnumerable<MessageSubscription> GetSubscribersForMessage<T>() where T : IMessage;

        IEnumerable<MessageSubscription> GetSubscribersForMessage(string messageName);

        string GetMessageKey<T>();
    }
}
