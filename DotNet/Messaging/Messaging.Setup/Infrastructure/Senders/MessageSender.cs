using System.Threading.Tasks;
using PaladinRogue.Libray.Messaging.Common.Messages;
using PaladinRogue.Libray.Messaging.Common.Senders;
using PaladinRogue.Libray.Messaging.Setup.Infrastructure.Directors;
using PaladinRogue.Libray.Messaging.Setup.Infrastructure.Factories;
using PaladinRogue.Libray.Messaging.Setup.Infrastructure.Interfaces;

namespace PaladinRogue.Libray.Messaging.Setup.Infrastructure.Senders
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
