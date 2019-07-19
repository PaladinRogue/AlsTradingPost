using Microsoft.Extensions.DependencyInjection;

namespace KeyVault.Setup.Infrastructure.Messaging
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMessageSubscribers(this IServiceCollection services)
        {
            return services;
        }
    }
}