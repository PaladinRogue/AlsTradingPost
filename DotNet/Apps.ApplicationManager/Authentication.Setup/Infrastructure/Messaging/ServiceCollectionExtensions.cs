using Authentication.Application.Handlers;
using Messaging.Setup.Infrastructure.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Setup.Infrastructure.Messaging
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMessageHandlers(this IServiceCollection services)
        {
            return services
                .AddScoped<IMessageHandler, SendNotificationMessageHandler>()
                .AddScoped<IMessageHandler, CreateAdminIdentityMessageHandler>()
                .AddScoped<IMessageHandler, AdminIdentityCreatedMessageHandler>()
                .AddScoped<IMessageHandler, AddAuthorisationClaimMessageHandler>();
        }
    }
}