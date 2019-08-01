using System;
using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Handlers;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.MessageBus
{
    public interface IMessageBus
    {
        void Publish(IMessage message);

        Task RegisterAsync<T, TH>(Func<T, Task> asyncHandler)
            where T : IMessage
            where TH : IMessageHandler<T>;

        Task UnregisterAsync<T, TH>()
            where T : IMessage
            where TH : IMessageHandler<T>;
    }
}
