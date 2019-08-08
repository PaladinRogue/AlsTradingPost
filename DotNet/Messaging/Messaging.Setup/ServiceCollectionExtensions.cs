using System;
using Messaging.Setup.Infrastructure.DeQueuers;
using Messaging.Setup.Infrastructure.Directors;
using Messaging.Setup.Infrastructure.Dispatchers;
using Messaging.Setup.Infrastructure.Factories;
using Messaging.Setup.Infrastructure.Handlers;
using Messaging.Setup.Infrastructure.MessageBus;
using Messaging.Setup.Infrastructure.Senders;
using Messaging.Setup.Infrastructure.Serialisers;
using Common.Resources.Settings;
using Messaging.RabbitMQ.Connection;
using Messaging.RabbitMQ.MessageBus;
using Messaging.RabbitMQ.Registrations;
using Messaging.Setup.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Common.Setup.Infrastructure.Messaging
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseRabbitMqMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MessagingBusSettings>(configuration.GetSection(nameof(MessagingBusSettings)));

            services.AddSingleton<IMessageBusRegistrationsManager, InMemoryMessageBusRegistrationsManager>();
            services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
            {
                ILogger<DefaultRabbitMqPersistentConnection> logger = sp.GetRequiredService<ILogger<DefaultRabbitMqPersistentConnection>>();

                IOptions<MessagingBusSettings> messageBusSettingsAccessor = sp.GetRequiredService<IOptions<MessagingBusSettings>>();
                MessagingBusSettings messageBusSettings = messageBusSettingsAccessor.Value;

                ConnectionFactory factory = new ConnectionFactory
                {
                    DispatchConsumersAsync = true,
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

                IMessageBusRegistrationsManager eventBusSubcriptionsManager = sp.GetRequiredService<IMessageBusRegistrationsManager>();
                IRabbitMqPersistentConnection rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMqPersistentConnection>();
                IMessageDeQueuer messageDeQueuer = sp.GetRequiredService<IMessageDeQueuer>();
                IMessageSerialiser messageSerialiser = sp.GetRequiredService<IMessageSerialiser>();

                int retryCount = messageBusSettings.RetryCount ?? 5;

                return new MessageBusRabbitMq(rabbitMqPersistentConnection, eventBusSubcriptionsManager, logger, messageDeQueuer, messageSerialiser, retryCount);
            });

            services.AddSingleton<IMessageHandlerFactory, MessageHandlerFactory>();
            services.AddSingleton<IMessageSerialiser, JsonMessageSerialiser>();

            services.AddSingleton<PendingMessageDirector>();
            services.AddScoped<IPendingMessageContainer>(sp => sp.GetRequiredService<PendingMessageDirector>());
            services.AddScoped<IPendingMessageProvider>(sp => sp.GetRequiredService<PendingMessageDirector>());

            services.AddScoped<IMessageDispatcher, MessageDispatcher>();
            services.AddScoped<IMessageDeQueuer, MessageDeQueuer>();
            services.AddScoped<IMessageSender, MessageSender>();
            services.AddScoped<IMessageFactory, MessageFactory>();

            return services;
        }
    }
}