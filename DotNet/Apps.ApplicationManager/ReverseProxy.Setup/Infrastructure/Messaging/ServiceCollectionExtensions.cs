using Common.Messaging.Infrastructure.Subscribers;
using Microsoft.Extensions.DependencyInjection;
using ReverseProxy.ApplicationServices.Subscribers;

namespace ReverseProxy.Setup.Infrastructure.Messaging
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