using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Messaging.Setup.Infrastructure;
using Messaging.Setup.Infrastructure.DeQueuers;
using Messaging.Setup.Infrastructure.Handlers;
using Messaging.Setup.Infrastructure.MessageBus;
using Messaging.Setup.Infrastructure.Serialisers;
using Messaging.Common;
using Messaging.RabbitMQ.Connection;
using Messaging.RabbitMQ.Registrations;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Messaging.RabbitMQ.MessageBus
{
    public class MessageBusRabbitMq : IMessageBus
    {
        private const string BrokerName = "paladin_rogue_message_bus";

        private readonly ILogger<MessageBusRabbitMq> _logger;
        private readonly IRabbitMqPersistentConnection _persistentConnection;
        private readonly IMessageBusRegistrationsManager _messageBusRegistrationsManager;
        private readonly IMessageDeQueuer _messageDeQueuer;
        private readonly IMessageSerialiser _messageSerialiser;
        private IModel _consumerChannel;
        private readonly int _retryCount;
        private string _queueName;

        public MessageBusRabbitMq(
            IRabbitMqPersistentConnection persistentConnection,
            IMessageBusRegistrationsManager messageBusRegistrationsManager,
            ILogger<MessageBusRabbitMq> logger,
            IMessageDeQueuer messageDeQueuer,
            IMessageSerialiser messageSerialiser,
            int retryCount = 5)
        {
            _persistentConnection = persistentConnection;
            _messageBusRegistrationsManager = messageBusRegistrationsManager;
            _logger = logger;
            _messageDeQueuer = messageDeQueuer;
            _messageSerialiser = messageSerialiser;

            _consumerChannel = CreateConsumerChannel();
            _retryCount = retryCount;
            _messageBusRegistrationsManager.OnMessageRemoved += RegistrationManagerOnMessageRemoved;
        }

        private void RegistrationManagerOnMessageRemoved(object sender, string messageName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (IModel channel = _persistentConnection.CreateModel())
            {
                channel.QueueUnbind(_queueName, BrokerName, messageName);

                if (_messageBusRegistrationsManager.IsEmpty)
                {
                    _queueName = string.Empty;
                    _consumerChannel.Close();
                }
            }
        }

        public void Publish(IMessage message)
        {
            if (!_persistentConnection.IsConnected)
            {
                if (!CanConnect()) return;
            }

            RetryPolicy policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, time) => { _logger.LogWarning(ex.ToString()); });

            using (IModel channel = _persistentConnection.CreateModel())
            {
                channel.ExchangeDeclare(
                    BrokerName,
                    "direct");

                byte[] body = Encoding.UTF8.GetBytes(_messageSerialiser.Serialise(message));

                policy.Execute(() => { channel.BasicPublish(BrokerName, message.Type, null, body); });
            }
        }

        public Task RegisterAsync<T, TH>(Func<T, Task> asyncHandler) where T : IMessage where TH : IMessageHandler<T>
        {
            string messageName = _messageBusRegistrationsManager.GetMessageKey<T>();
            DoInternalRegistration(messageName);
            return _messageBusRegistrationsManager.AddRegistrationAsync<T, TH>(asyncHandler);
        }

        private void DoInternalRegistration(string messageName)
        {
            if (_messageBusRegistrationsManager.HasRegistrationsForMessage(messageName)) return;

            if (!_persistentConnection.IsConnected)
            {
                if (!CanConnect()) return;
            }

            using (IModel channel = _persistentConnection.CreateModel())
            {
                channel.QueueBind(_queueName, BrokerName, messageName);
            }
        }

        public Task UnregisterAsync<T, TH>() where T : IMessage where TH : IMessageHandler<T>
        {
            return _messageBusRegistrationsManager.RemoveRegistrationAsync<T, TH>();
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                if (!CanConnect()) return null;
            }

            IModel channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(BrokerName, "direct");

            _queueName = channel.QueueDeclare().QueueName;

            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += ConsumerRecieved;

            channel.BasicConsume(_queueName, false, consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };

            return channel;
        }

        private async Task ConsumerRecieved(object sender, BasicDeliverEventArgs @event)
        {
            string messageKey = @event.RoutingKey;
            string message = Encoding.UTF8.GetString(@event.Body);

            await ProcessMessageAsync(messageKey, message);
        }

        private async Task ProcessMessageAsync(
            string messageName,
            string serialisedMessage)
        {
            if (!_messageBusRegistrationsManager.HasRegistrationsForMessage(messageName))
            {
                return;
            }

            try
            {
                IEnumerable<MessageRegistration> registrations = _messageBusRegistrationsManager.GetRegistrationsForMessage(messageName);
                await _messageDeQueuer.DeQueueAsync(_messageSerialiser.Deserialise(serialisedMessage), registrations);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
            }
        }

        private bool CanConnect()
        {
            try
            {
                _persistentConnection.TryConnect();
            }
            catch (Exception e)
            {
                _logger.LogCritical("Unable to connect to RabbitMQ", e);
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            _consumerChannel?.Dispose();

            _messageBusRegistrationsManager.Clear();
        }
    }
}