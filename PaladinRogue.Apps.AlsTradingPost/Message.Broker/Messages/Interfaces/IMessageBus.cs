using System;
using Common.Messaging.Interfaces;

namespace Message.Broker.Messages.Interfaces
{
    public interface IMessageBus
    {
        void Publish(IMessage message);

        void Subscribe<T, TH>(Action<T> handler)
            where T : IMessage
            where TH : IMessageSubscriber<T>;

        void Unsubscribe<T, TH>()
            where TH : IMessageSubscriber<T>
            where T : IMessage;
    }
}
