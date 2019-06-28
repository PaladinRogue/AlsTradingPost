using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Common.Messaging.Infrastructure;
using Common.Messaging.Infrastructure.DeQueuers;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Infrastructure.Messages;
using Common.Messaging.Infrastructure.Serialisers;
using Common.Messaging.Infrastructure.Subscribers;
using Messaging.Broker.Connection;
using Messaging.Broker.Subscriptions;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Messaging.Broker.MessageBus
{
    public class MessageBusRabbitMq : IMessageBus
    {
        private const string BrokerName = "paladin_rogue_message_bus";

        private readonly ILogger<MessageBusRabbitMq> _logger;
        private readonly IRabbitMqPersistentConnection _persistentConnection;
        private readonly IMessageBusSubscriptionsManager _messageBusSubscriptionsManager;
        private readonly IMessageDeQueuer _messageDeQueuer;
        private readonly IMessageSerialiser _messageSerialiser;
        private IModel _consumerChannel;
        private readonly int _retryCount;
        private string _queueName;

        public MessageBusRabbitMq(
            IRabbitMqPersistentConnection persistentConnection,
            IMessageBusSubscriptionsManager messageBusSubscriptionsManager,
            ILogger<MessageBusRabbitMq> logger,
            IMessageDeQueuer messageDeQueuer,
            IMessageSerialiser messageSerialiser,
            int retryCount = 5)
        {
            _persistentConnection = persistentConnection;
            _messageBusSubscriptionsManager = messageBusSubscriptionsManager;
            _logger = logger;
            _messageDeQueuer = messageDeQueuer;
            _messageSerialiser = messageSerialiser;

            _consumerChannel = CreateConsumerChannel();
            _retryCount = retryCount;
            _messageBusSubscriptionsManager.OnMessageRemoved += SubscriptionManagerOnMessageRemoved;
        }

        private void SubscriptionManagerOnMessageRemoved(object sender, string messageName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (IModel channel = _persistentConnection.CreateModel())
            {
                channel.QueueUnbind(
                    queue: _queueName,
                    exchange: BrokerName,
                    routingKey: messageName);

                if (_messageBusSubscriptionsManager.IsEmpty)
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
                    exchange: BrokerName,
                    type: "direct");

                byte[] body = Encoding.UTF8.GetBytes(_messageSerialiser.Serialise(message));

                policy.Execute(() =>
                {
                    channel.BasicPublish(
                        exchange: BrokerName,
                        routingKey: message.Type,
                        basicProperties: null,
                        body: body);
                });
            }
        }

        public void Subscribe<T, TH>(Action<T> handler) where T : IMessage where TH : IMessageSubscriber<T>
        {
            string messageName = _messageBusSubscriptionsManager.GetMessageKey<T>();
            DoInternalSubscription(messageName);
            _messageBusSubscriptionsManager.AddSubscription<T, TH>(handler);
        }

        private void DoInternalSubscription(string messageName)
        {
            if (_messageBusSubscriptionsManager.HasSubscriptionsForMessage(messageName)) return;

            if (!_persistentConnection.IsConnected)
            {
                if (!CanConnect()) return;
            }

            using (IModel channel = _persistentConnection.CreateModel())
            {
                channel.QueueBind(
                    queue: _queueName,
                    exchange: BrokerName,
                    routingKey: messageName);
            }
        }

        public void Unsubscribe<T, TH>() where T : IMessage where TH : IMessageSubscriber<T>
        {
            _messageBusSubscriptionsManager.RemoveSubscription<T, TH>();
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                if (!CanConnect()) return null;
            }

            IModel channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(
                exchange: BrokerName,
                type: "direct");

            _queueName = channel.QueueDeclare().QueueName;

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                string messageKey = ea.RoutingKey;
                string message = Encoding.UTF8.GetString(ea.Body);

                ProcessMessage(messageKey, message);
            };

            channel.BasicConsume(
                queue: _queueName,
                autoAck: false,
                consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };

            return channel;
        }

        private void ProcessMessage(string messageName, string serialisedMessage)
        {
            if (!_messageBusSubscriptionsManager.HasSubscriptionsForMessage(messageName))
            {
                return;
            }

            IEnumerable<MessageSubscription> subscriptions = _messageBusSubscriptionsManager.GetSubscribersForMessage(messageName);
            _messageDeQueuer.DeQueue(_messageSerialiser.Deserialise(serialisedMessage), subscriptions);
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

            _messageBusSubscriptionsManager.Clear();
        }
    }
}