using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common.Messaging.Message.Interfaces;
using Message.Broker.Connection.Interfaces;
using Message.Broker.Messages.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Message.Broker.Messages
{
    public class MessageBusRabbitMq : IMessageBus
    {
        private const string BrokerName = "paladin_rogue_message_bus";

        private readonly ILogger<MessageBusRabbitMq> _logger;
        private readonly IRabbitMqPersistentConnection _persistentConnection;
        private readonly JsonSerializerSettings _settings;
        private readonly IMessageBusSubscriptionsManager _messageBusSubscriptionsManager;
        private IModel _consumerChannel;
        private readonly int _retryCount;
        private string _queueName;

        public MessageBusRabbitMq(IRabbitMqPersistentConnection persistentConnection,
            IMessageBusSubscriptionsManager messageBusSubscriptionsManager,
            ILogger<MessageBusRabbitMq> logger,
            int retryCount = 5)
        {
            _persistentConnection = persistentConnection;
            _messageBusSubscriptionsManager = messageBusSubscriptionsManager;
            _logger = logger;

            _consumerChannel = CreateConsumerChannel();
            _settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
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
                _persistentConnection.TryConnect();
            }

            RetryPolicy policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, time) => { _logger.LogWarning(ex.ToString()); });

            using (IModel channel = _persistentConnection.CreateModel())
            {
                string messageName = message.GetType().Name;

                channel.ExchangeDeclare(
                    exchange: BrokerName,
                    type: "direct");

                string serializedMessage = JsonConvert.SerializeObject(message, _settings);
                byte[] body = Encoding.UTF8.GetBytes(serializedMessage);

                policy.Execute(() =>
                {
                    channel.BasicPublish(
                        exchange: BrokerName,
                        routingKey: messageName,
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
                _persistentConnection.TryConnect();
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
                _persistentConnection.TryConnect();
            }

            IModel channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(
                exchange: BrokerName,
                type: "direct");

            _queueName = channel.QueueDeclare().QueueName;

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                string messageKey = ea.RoutingKey;
                string message = Encoding.UTF8.GetString(ea.Body);

                await ProcessMessage(messageKey, message);
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

        private async Task ProcessMessage(string messageName, string serializedMessage)
        {
            if (_messageBusSubscriptionsManager.HasSubscriptionsForMessage(messageName))
            {
                IEnumerable<MessageSubscription> subscriptions = _messageBusSubscriptionsManager.GetSubscribersForMessage(messageName);

                await Task.Run(() => Parallel.ForEach(subscriptions,
                    subscription =>
                    {
                        IMessage message = JsonConvert.DeserializeObject<IMessage>(serializedMessage, _settings);
                        subscription.Handler.DynamicInvoke(message);
                    }));
            }
        }

        public void Dispose()
        {
            _consumerChannel?.Dispose();

            _messageBusSubscriptionsManager.Clear();
        }
    }
}