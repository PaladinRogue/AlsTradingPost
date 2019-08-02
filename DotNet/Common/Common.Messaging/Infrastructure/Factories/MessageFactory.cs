using System;
using System.Threading.Tasks;
using Common.Domain.DataProtectors;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Messages;
using Common.Messaging.Infrastructure.Serialisers;

namespace Common.Messaging.Infrastructure.Factories
{
    public class MessageFactory : IMessageFactory
    {
        private readonly IDataProtector _dataProtector;

        private readonly IMessageSerialiser _messageSerialiser;

        public MessageFactory(
            IDataProtector dataProtector,
            IMessageSerialiser messageSerialiser)
        {
            _dataProtector = dataProtector;
            _messageSerialiser = messageSerialiser;
        }

        public async Task<IPreparedMessage> CreateAsync(IMessage message)
        {
            Guid id = Guid.NewGuid();

            return PreparedMessage.Create(id, message.Type,
                await _dataProtector.ProtectAsync(id, SharedDataKeys.Queue),
                await _dataProtector.ProtectAsync(_messageSerialiser.Serialise(message), SharedDataKeys.Queue)
            );
        }
    }
}