using Common.Messaging.Infrastructure.Directors;
using Common.Messaging.Infrastructure.Interfaces;

namespace Common.Messaging.Infrastructure.Senders
{
    public class MessageSender : IMessageSender
    {
        private readonly IPendingMessageContainer _pendingMessageContainer;

        private readonly IMessageFactory _messageFactory;

        public MessageSender(
            IMessageFactory messageFactory,
            IPendingMessageContainer pendingMessageContainer)
        {
            _messageFactory = messageFactory;
            _pendingMessageContainer = pendingMessageContainer;
        }

        public void Send(IMessage message)
        {
            IPreparedMessage preparedMessage = _messageFactory.Create(message);

            _pendingMessageContainer.Add(preparedMessage);
        }
    }
}
