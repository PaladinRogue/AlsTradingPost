using System;
using System.Collections.Generic;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.Domain.Models.DataProtection;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Serialisers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common.Messaging.Infrastructure.DeQueuers
{
    public class MessageDeQueuer : IMessageDeQueuer
    {
        private readonly IDataProtector _dataProtector;

        private readonly ILogger<MessageDeQueuer> _logger;

        private readonly IMessageSerialiser _messageSerialiser;

        private readonly IServiceProvider _serviceProvider;
        private readonly ITransactionManager _transactionManager;

        public MessageDeQueuer(
            IDataProtector dataProtector,
            ILogger<MessageDeQueuer> logger,
            IMessageSerialiser messageSerialiser,
            IServiceProvider serviceProvider,
            ITransactionManager transactionManager)
        {
            _dataProtector = dataProtector;
            _logger = logger;
            _messageSerialiser = messageSerialiser;
            _serviceProvider = serviceProvider;
            _transactionManager = transactionManager;
        }

        public void DeQueue(IMessage message, IEnumerable<MessageSubscription> messageSubscriptions)
        {
            if (message is IPreparedMessage preparedMessage)
            {
                using (_serviceProvider.CreateScope())
                {
                    using (ITransaction transaction = _transactionManager.Create())
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

                            foreach (MessageSubscription messageSubscription in messageSubscriptions)
                            {
                                messageSubscription.Handler.DynamicInvoke(deserialisedMessage);
                            }

                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            _logger.LogCritical(e.Message);
                        }
                    }
                }
            }
            else
            {
                throw new BusinessApplicationException(ExceptionType.Unknown, "Unable to verify sender of message");
            }
        }
    }
}
