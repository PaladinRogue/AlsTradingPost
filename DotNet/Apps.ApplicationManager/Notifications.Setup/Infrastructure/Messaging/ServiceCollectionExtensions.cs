using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Messaging.Common.Handlers;
using PaladinRogue.Notifications.Application.Handlers;

namespace PaladinRogue.Notifications.Setup.Infrastructure.Messaging
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