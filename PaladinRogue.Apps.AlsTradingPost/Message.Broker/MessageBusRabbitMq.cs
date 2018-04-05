using System;
using System.Net.Sockets;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;
using Common.Messaging.Interfaces;
using Message.Broker.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using IMessageBus = Message.Broker.Interfaces.IMessageBus;

namespace Message.Broker
{
    public class MessageBusRabbitMq : IMessageBus
    {
        const string BrokerName = "paladin_rogue_message_bus";

        private readonly ILogger<MessageBusRabbitMq> _logger;
        private readonly IRabbitMqPersistentConnection _persistentConnection;
        private readonly JsonSerializerSettings _settings;
        private readonly IMessageBusSubscriptionsManager _messageBusSubscriptionsManager;
        private IModel _consumerChannel;
        private readonly int _retryCount;
        private string _queueName;

        public MessageBusRabbitMq(IRabbitMqPersistentConnection persistentConnection,
            IMessageBusSubscriptionsManager messageBusSubscriptionsManager, ILogger<MessageBusRabbitMq> logger,
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
            _messageBusSubscriptionsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueUnbind(queue: _queueName,
                    exchange: BrokerName,
                    routingKey: eventName);

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

            using (var channel = _persistentConnection.CreateModel())
            {
                var eventName = message.GetType()
                    .Name;

                channel.ExchangeDeclare(exchange: BrokerName,
                    type: "direct");

                var serializedMessage = JsonConvert.SerializeObject(message, _settings);
                var body = Encoding.UTF8.GetBytes(serializedMessage);

                policy.Execute(() =>
                {
                    channel.BasicPublish(exchange: BrokerName,
                        routingKey: eventName,
                        basicProperties: null,
                        body: body);
                });
            }
        }

        public void Subscribe<T, TH>(Action<T> handler) where T : IMessage where TH : IMessageSubscriber<T>
        {
            var eventName = _messageBusSubscriptionsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);
            _messageBusSubscriptionsManager.AddSubscription<T, TH>(handler);
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _messageBusSubscriptionsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                using (var channel = _persistentConnection.CreateModel())
                {
                    channel.QueueBind(queue: _queueName,
                        exchange: BrokerName,
                        routingKey: eventName);
                }
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

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BrokerName,
                type: "direct");

            _queueName = channel.QueueDeclare().QueueName;

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body);

                await ProcessEvent(eventName, message);
            };

            channel.BasicConsume(queue: _queueName,
                autoAck: false,
                consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };

            return channel;
        }

        private async Task ProcessEvent(string eventName, string serializedMessage)
        {
            if (_messageBusSubscriptionsManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = _messageBusSubscriptionsManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    IMessage message = JsonConvert.DeserializeObject<IMessage>(serializedMessage, _settings);
                    await Task.Run(() => subscription.Handler.DynamicInvoke(message));

                }
            }
        }

        public void Dispose()
        {
            _consumerChannel?.Dispose();

            _messageBusSubscriptionsManager.Clear();
        }
    }
}