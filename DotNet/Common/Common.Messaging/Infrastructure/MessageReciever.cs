using System;
using Common.Application.Exceptions;
using Common.Domain.Models.DataProtection;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Serialisers;
using Microsoft.Extensions.Logging;

namespace Common.Messaging.Infrastructure
{
    public class MessageReciever : IMessageReciever
    {
        private readonly IDataProtector _dataProtector;

        private readonly ILogger<MessageReciever> _logger;

        private readonly IMessageSerialiser _messageSerialiser;

        public MessageReciever(
            IDataProtector dataProtector,
            ILogger<MessageReciever> logger,
            IMessageSerialiser messageSerialiser)
        {
            _dataProtector = dataProtector;
            _logger = logger;
            _messageSerialiser = messageSerialiser;
        }

        public void Recieve(IMessage message, MessageSubscription messageSubscription)
        {
            if (message is IPreparedMessage preparedMessage)
            {
                try
                {
                    if (_dataProtector.Unprotect<Guid>(preparedMessage.SecurityToken) != preparedMessage.Id)
                    {
                        throw new BusinessApplicationException(ExceptionType.Unknown,
                            "Unable to verify sender of message");
                    }

                    string unprotectedMessage = _dataProtector.Unprotect<string>(preparedMessage.Payload);
                    IMessage deserialisedMessage = _messageSerialiser.Deserialise(unprotectedMessage);

                    messageSubscription.Handler.DynamicInvoke(deserialisedMessage);
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e.Message);
                }
            }
            else
            {
                throw new BusinessApplicationException(ExceptionType.Unknown, "Unable to verify sender of message");
            }
        }
    }
}
