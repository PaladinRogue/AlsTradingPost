using Common.Messaging;
using Common.Messaging.Interfaces;
using Common.Setup.Settings;
using Message.Broker;
using Message.Broker.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Common.Setup
{
    public static class MessageRegistration
    {
	    public static void RegisterRabbitMqMessaging(IServiceCollection services)
	    {
            services.AddSingleton<IMessageBusSubscriptionsManager, InMemoryMessageBusSubscriptionsManager>();
	        services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
	        {
	            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMqPersistentConnection>>();
	            var messageSettingsAccessor = sp.GetRequiredService<IOptions<MessagingBusSettings>>();

	            MessagingBusSettings messageBusSettings = messageSettingsAccessor.Value;

                var factory = new ConnectionFactory
	            {
	                HostName = string.IsNullOrEmpty(messageBusSettings.Connection) ? "localhost" : messageBusSettings.Connection
	            };

	            if (!string.IsNullOrEmpty(messageBusSettings.UserName))
	            {
	                factory.UserName = messageBusSettings.UserName;
	            }

	            if (!string.IsNullOrEmpty(messageBusSettings.Password))
	            {
	                factory.Password = messageBusSettings.Password;
	            }


	            var retryCount = messageBusSettings.RetryCount ?? 5;

                return new DefaultRabbitMqPersistentConnection(factory, logger, retryCount);
	        });

            services.AddSingleton<IMessageBus, MessageBusRabbitMq>(sp =>
	        {
	            IRabbitMqPersistentConnection rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMqPersistentConnection>();
	            var logger = sp.GetRequiredService<ILogger<MessageBusRabbitMq>>();
	            IMessageBusSubscriptionsManager eventBusSubcriptionsManager = sp.GetRequiredService<IMessageBusSubscriptionsManager>();
	            var messageSettingsAccessor = sp.GetRequiredService<IOptions<MessagingBusSettings>>();

	            MessagingBusSettings messageBusSettings = messageSettingsAccessor.Value;

	            var retryCount = messageBusSettings.RetryCount ?? 5;


                return new MessageBusRabbitMq(rabbitMqPersistentConnection, eventBusSubcriptionsManager, logger, retryCount);
	        });

            services.AddSingleton<IMessageSubscribers, MessageSubscribers>();
		    services.AddScoped<IMessages, Messages>();
			services.AddScoped<IMessageDispatcher, MessageDispatcher>();
	    }
    }
}
