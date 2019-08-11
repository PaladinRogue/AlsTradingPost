using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Gateway.Application.Handlers;
using PaladinRogue.Libray.Messaging.Common.Handlers;

namespace PaladinRogue.Gateway.Setup.Infrastructure.Messaging
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMessageHandlers(this IServiceCollection services)
        {
            return services
                .AddScoped<IMessageHandler, RegisterApplicationMessageHandler>();
        }
    }
}