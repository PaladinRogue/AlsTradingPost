using System.Threading.Tasks;
using Messaging.Common;
using Messaging.Setup.Infrastructure.Directors;
using Messaging.Setup.Infrastructure.Factories;
using Messaging.Setup.Infrastructure.Interfaces;

namespace Messaging.Setup.Infrastructure.Senders
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
