using System;
using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Messages;
using Common.Messaging.Infrastructure.Subscribers;

namespace Common.Messaging.Infrastructure.MessageBus
{
    public interface IMessageBus
    {
        void Publish(IMessage message);

        Task SubscribeAsync<T, TH>(Func<T, Task> asyncHandler)
            where T : IMessage
            where TH : IMessageSubscriber<T>;

        Task UnsubscribeAsync<T, TH>()
            where T : IMessage
            where TH : IMessageSubscriber<T>;
    }
}
