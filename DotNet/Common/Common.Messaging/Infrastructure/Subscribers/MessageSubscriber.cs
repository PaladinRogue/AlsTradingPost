using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Subscribers
{
    public abstract class MessageSubscriber<T, TSubscriber> : IMessageSubscriber, IMessageSubscriber<T>
        where T: IMessage
        where TSubscriber : IMessageSubscriber<T>
    {
        private readonly IMessageBus _messageBus;

        protected MessageSubscriber(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public abstract void Handle(T message);

        public void Subscribe()
        {
            _messageBus.Subscribe<T, TSubscriber>(Handle);
        }
    }
}
