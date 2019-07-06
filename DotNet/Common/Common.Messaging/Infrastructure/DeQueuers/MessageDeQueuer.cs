using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.DataProtection;
using Common.Messaging.Infrastructure.Dispatchers;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Messages;
using Common.Messaging.Infrastructure.Serialisers;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Messaging.Infrastructure.DeQueuers
{
    public class MessageDeQueuer : IMessageDeQueuer
    {
        private readonly IDataProtector _dataProtector;

        private readonly IMessageSerialiser _messageSerialiser;

        private readonly IServiceProvider _serviceProvider;

        private readonly ITransactionManager _transactionManager;

        private IMessageDispatcher _messageDispatcher;

        public MessageDeQueuer(
            IDataProtector dataProtector,
            IMessageSerialiser messageSerialiser,
            IServiceProvider serviceProvider,
            ITransactionManager transactionManager)
        {
            _dataProtector = dataProtector;
            _messageSerialiser = messageSerialiser;
            _serviceProvider = serviceProvider;
            _transactionManager = transactionManager;
        }

        public async Task DeQueueAsync(
            IMessage message,
            IEnumerable<MessageSubscription> messageSubscriptions)
        {
            if (message is IPreparedMessage preparedMessage)
            {
                using (_serviceProvider.CreateScope())
                {
                    if (_dataProtector.Unprotect<Guid>(preparedMessage.SecurityToken) != preparedMessage.Id)
                    {
                        throw new BusinessApplicationException(ExceptionType.Unknown, "Unable to verify sender of message");
                    }

                    string unprotectedMessage = _dataProtector.Unprotect<string>(preparedMessage.Payload);
                    IMessage deserialisedMessage = _messageSerialiser.Deserialise(unprotectedMessage);

                    using (ITransaction transaction = _transactionManager.Create())
                    {
                        try
                        {
                            foreach (MessageSubscription messageSubscription in messageSubscriptions)
                            {
                                await (Task) messageSubscription.AsyncHandler.DynamicInvoke(deserialisedMessage);

                                transaction.Commit();
                            }
                        }
                        catch (Exception e)
                        {
                            throw new BusinessApplicationException(ExceptionType.Unknown, $"Unable to dequeue message for type: {message.Type}", e);
                        }
                    }

                    using (ITransaction transaction = _transactionManager.Create())
                    {
                        try
                        {
                            _messageDispatcher = _serviceProvider.GetRequiredService<IMessageDispatcher>();

                            await _messageDispatcher.DispatchMessagesAsync();

                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            throw new BusinessApplicationException(ExceptionType.Unknown, $"Unable to forward messages for type: {message.Type}", e);
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
