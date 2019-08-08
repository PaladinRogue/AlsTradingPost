using System.Threading.Tasks;
using Messaging.Common;
using Messaging.Setup.Infrastructure.MessageBus;

namespace Messaging.Setup.Infrastructure.Handlers
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
