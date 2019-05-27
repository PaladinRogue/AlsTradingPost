using System;
using Common.Messaging.Message;
using Common.Messaging.Message.Interfaces;
using Common.Setup.Settings;
using Message.Broker.Connection;
using Message.Broker.Connection.Interfaces;
using Message.Broker.Messages;
using Message.Broker.Messages.Interfaces;
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
	            ILogger<DefaultRabbitMqPersistentConnection> logger = sp.GetRequiredService<ILogger<DefaultRabbitMqPersistentConnection>>();

	            IOptions<MessagingBusSettings> messageBusSettingsAccessor = sp.GetRequiredService<IOptions<MessagingBusSettings>>();
	            MessagingBusSettings messageBusSettings = messageBusSettingsAccessor.Value;

                ConnectionFactory factory = new ConnectionFactory
	            {
	                HostName = string.IsNullOrEmpty(messageBusSettings.Connection) ? throw new ArgumentException(nameof(messageBusSettings.Connection)) : messageBusSettings.Connection
	            };

	            if (!string.IsNullOrEmpty(messageBusSettings.UserName))
	            {
	                factory.UserName = messageBusSettings.UserName;
	            }

	            if (!string.IsNullOrEmpty(messageBusSettings.Password))
	            {
	                factory.Password = messageBusSettings.Password;
	            }


	            int retryCount = messageBusSettings.RetryCount ?? 5;

                return new DefaultRabbitMqPersistentConnection(factory, logger, retryCount);
	        });

            services.AddSingleton<IMessageBus, MessageBusRabbitMq>(sp =>
	        {
	            ILogger<MessageBusRabbitMq> logger = sp.GetRequiredService<ILogger<MessageBusRabbitMq>>();
	            MessagingBusSettings messageBusSettings = sp.GetRequiredService<IOptions<MessagingBusSettings>>().Value;

                IServiceProvider serviceProvider = sp.GetRequiredService<IServiceProvider>();
                IMessageBusSubscriptionsManager eventBusSubcriptionsManager = sp.GetRequiredService<IMessageBusSubscriptionsManager>();
                IRabbitMqPersistentConnection rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMqPersistentConnection>();

	            int retryCount = messageBusSettings.RetryCount ?? 5;
                
                return new MessageBusRabbitMq(rabbitMqPersistentConnection, eventBusSubcriptionsManager, logger, serviceProvider, retryCount);
	        });

            services.AddSingleton<IMessageSubscriberFactory, MessageSubscriberFactory>();

	        PendingMessageDirector pendingMessageDirector = new PendingMessageDirector();
            services.AddScoped<IPendingMessageContainer>(sp => pendingMessageDirector);
		    services.AddScoped<IPendingMessageProvider>(sp => pendingMessageDirector);
			services.AddScoped<IMessageDispatcher, MessageDispatcher>();
	    }
    }
}
