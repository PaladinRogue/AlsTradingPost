using Common.Messaging.Infrastructure.Subscribers;
using Gateway.ApplicationServices.Subscribers;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Setup.Infrastructure.Messaging
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMessageSubscribers(this IServiceCollection services)
        {
            return services
                .AddScoped<IMessageSubscriber, RegisterApplicationMessageSubscriber>();
        }
    }
}