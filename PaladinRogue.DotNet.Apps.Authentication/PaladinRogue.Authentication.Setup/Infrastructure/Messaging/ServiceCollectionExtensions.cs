using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Authentication.Application.Handlers;
using PaladinRogue.Library.Messaging.Common.Handlers;

namespace PaladinRogue.Authentication.Setup.Infrastructure.Messaging
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