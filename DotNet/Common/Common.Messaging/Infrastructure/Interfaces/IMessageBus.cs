using System;
using Common.Messaging.Subscribers;

namespace Common.Messaging.Infrastructure.Interfaces
{
    public interface IMessageBus
    {
        void Publish(IMessage message);

        void Subscribe<T, TH>(Action<T> handler)
            where T : IMessage
            where TH : IMessageSubscriber<T>;

        void Unsubscribe<T, TH>()
            where T : IMessage
            where TH : IMessageSubscriber<T>;
    }
}
