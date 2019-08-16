using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Core.Application.Exceptions;
using PaladinRogue.Library.Core.Application.Transactions;
using PaladinRogue.Library.Core.Domain.DataProtectors;
using PaladinRogue.Library.Messaging.Common.Dispatchers;
using PaladinRogue.Library.Messaging.Common.MessageDequers;
using PaladinRogue.Library.Messaging.Common.Messages;
using PaladinRogue.Library.Messaging.Common.Registrations;
using PaladinRogue.Library.Messaging.Common.Serialisers;
using PaladinRogue.Library.Messaging.Setup.Infrastructure.Interfaces;

namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.DeQueuers
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
            IEnumerable<MessageRegistration> messageRegistrations)
        {
            if (message is IPreparedMessage preparedMessage)
            {
                if (await _dataProtector.UnprotectAsync<Guid>(preparedMessage.SecurityToken, SharedDataKeys.Queue) != preparedMessage.Id)
                {
                    throw new BusinessApplicationException(ExceptionType.Unknown, "Unable to verify sender of message");
                }

                string unprotectedMessage = await _dataProtector.UnprotectAsync<string>(preparedMessage.Payload, SharedDataKeys.Queue);
                IMessage deserialisedMessage = _messageSerialiser.Deserialise(unprotectedMessage);

                using (_serviceProvider.CreateScope())
                {
                    using (ITransaction transaction = _transactionManager.Create())
                    {
                        try
                        {
                            foreach (MessageRegistration messageRegistration in messageRegistrations)
                            {
                                await (Task) messageRegistration.AsyncHandler.DynamicInvoke(deserialisedMessage);

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