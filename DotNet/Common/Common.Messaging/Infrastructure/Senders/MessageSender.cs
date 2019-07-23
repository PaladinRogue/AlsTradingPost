using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Directors;
using Common.Messaging.Infrastructure.Factories;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Messages;

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

        public async Task SendAsync(IMessage message)
        {
            IPreparedMessage preparedMessage = await _messageFactory.CreateAsync(message);

            _pendingMessageContainer.Add(preparedMessage);
        }
    }
}
