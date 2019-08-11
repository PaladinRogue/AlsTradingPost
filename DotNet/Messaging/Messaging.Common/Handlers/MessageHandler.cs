using System.Threading.Tasks;
using PaladinRogue.Libray.Messaging.Common.MessageBus;
using PaladinRogue.Libray.Messaging.Common.Messages;

namespace PaladinRogue.Libray.Messaging.Common.Handlers
{
    public abstract class MessageHandler<T, TMessageHandler> : IMessageHandler, IMessageHandler<T>
        where T: IMessage
        where TMessageHandler : IMessageHandler<T>
    {
        private readonly IMessageBus _messageBus;

        protected MessageHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public abstract Task ExecuteAsync(T message);

        public Task RegisterAsync()
        {
            return _messageBus.RegisterAsync<T, TMessageHandler>(ExecuteAsync);
        }
    }
}
