﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Messaging.Infrastructure;
using Common.Messaging.Infrastructure.Messages;
using Common.Messaging.Infrastructure.Subscribers;

namespace Messaging.Broker.Subscriptions
{
    public interface IMessageBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnMessageRemoved;

        void AddSubscription<T, TH>(Func<T, Task> asyncHandler)
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
