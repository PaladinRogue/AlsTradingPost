using System.Threading.Tasks;
using PaladinRogue.Library.Messaging.Common.Messages;
using PaladinRogue.Library.Messaging.Common.Senders;
using PaladinRogue.Library.Messaging.Setup.Infrastructure.Directors;
using PaladinRogue.Library.Messaging.Setup.Infrastructure.Factories;
using PaladinRogue.Library.Messaging.Setup.Infrastructure.Interfaces;

namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.Senders
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
