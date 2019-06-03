using System;
using Common.Domain.Models.DataProtection;
using Common.Messaging.Message.Interfaces;
using Common.Messaging.Serialisers;

namespace Common.Messaging.Message
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

        public IPreparedMessage Create(IMessage message)
        {
            Guid id = Guid.NewGuid();

            return Message.Create(id, message.Type, _dataProtector.Protect(id), _dataProtector.Protect(_messageSerialiser.Serialise(message)));
        }
    }
}
