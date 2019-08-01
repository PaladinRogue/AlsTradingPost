using Microsoft.Extensions.DependencyInjection;

namespace Vault.Setup.Infrastructure.Messaging
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMessageHandlers(this IServiceCollection services)
        {
            return services;
        }
    }
}