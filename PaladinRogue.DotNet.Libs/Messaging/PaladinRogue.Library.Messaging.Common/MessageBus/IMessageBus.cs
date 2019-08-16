using System;
using System.Threading.Tasks;
using PaladinRogue.Library.Messaging.Common.Handlers;
using PaladinRogue.Library.Messaging.Common.Messages;

namespace PaladinRogue.Library.Messaging.Common.MessageBus
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
