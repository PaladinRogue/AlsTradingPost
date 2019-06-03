using Common.Messaging.Message.Interfaces;

namespace Common.Messaging.Message
{
    public class MessageSender : IMessageSender
    {

        private readonly IMessageBus _messageBus;

        private readonly IMessageFactory _messageFactory;

        public MessageSender(
            IMessageBus messageBus,
            IMessageFactory messageFactory)
        {
            _messageBus = messageBus;
            _messageFactory = messageFactory;
        }

        public void Send(IMessage message)
        {
            IPreparedMessage preparedMessage = _messageFactory.Create(message);

            _messageBus.Publish(preparedMessage);
        }
    }
}
