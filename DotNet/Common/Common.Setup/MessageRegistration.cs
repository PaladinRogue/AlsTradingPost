using System;
using Common.Messaging.Infrastructure;
using Common.Messaging.Infrastructure.DeQueuers;
using Common.Messaging.Infrastructure.Directors;
using Common.Messaging.Infrastructure.Dispatchers;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Senders;
using Common.Messaging.Infrastructure.Serialisers;
using Common.Messaging.Infrastructure.Subscribers;
using Common.Setup.Settings;
using Messaging.Broker.Connection;
using Messaging.Broker.MessageBus;
using Messaging.Broker.Subscriptions;
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

                IMessageBusSubscriptionsManager eventBusSubcriptionsManager = sp.GetRequiredService<IMessageBusSubscriptionsManager>();
                IRabbitMqPersistentConnection rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMqPersistentConnection>();
                IMessageDeQueuer messageDeQueuer = sp.GetRequiredService<IMessageDeQueuer>();
                IMessageSerialiser messageSerialiser = sp.GetRequiredService<IMessageSerialiser>();

	            int retryCount = messageBusSettings.RetryCount ?? 5;

                return new MessageBusRabbitMq(rabbitMqPersistentConnection, eventBusSubcriptionsManager, logger, messageDeQueuer, messageSerialiser, retryCount);
	        });

            services.AddSingleton<IMessageSubscriberFactory, MessageSubscriberFactory>();
	        services.AddSingleton<IMessageSerialiser, JsonMessageSerialiser>();

            services.AddSingleton<PendingMessageDirector>();
            services.AddScoped<IPendingMessageContainer>(sp => sp.GetRequiredService<PendingMessageDirector>());
		    services.AddScoped<IPendingMessageProvider>(sp => sp.GetRequiredService<PendingMessageDirector>());

			services.AddScoped<IMessageDispatcher, MessageDispatcher>();
            services.AddScoped<IMessageDeQueuer, MessageDeQueuer>();
			services.AddScoped<IMessageSender, MessageSender>();
			services.AddScoped<IMessageFactory, MessageFactory>();
	    }
    }
}
