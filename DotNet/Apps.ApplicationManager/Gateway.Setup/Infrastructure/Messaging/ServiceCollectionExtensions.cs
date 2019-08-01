using Common.Messaging.Infrastructure.Handlers;
using Gateway.ApplicationServices.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Setup.Infrastructure.Messaging
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