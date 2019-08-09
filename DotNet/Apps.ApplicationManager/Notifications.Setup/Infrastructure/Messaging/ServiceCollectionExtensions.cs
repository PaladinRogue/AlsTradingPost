﻿using Messaging.Setup.Infrastructure.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Handlers;

namespace Notifications.Setup.Infrastructure.Messaging
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMessageHandlers(this IServiceCollection services)
        {
            return services
                .AddScoped<IMessageHandler, SendEmailNotificationMessageHandler>();
        }
    }
}