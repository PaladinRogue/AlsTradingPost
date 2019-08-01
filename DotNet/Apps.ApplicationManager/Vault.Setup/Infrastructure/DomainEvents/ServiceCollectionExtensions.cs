using Microsoft.Extensions.DependencyInjection;

namespace Vault.Setup.Infrastructure.DomainEvents
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainEventSubscribers(this IServiceCollection services)
        {
            return services;
        }
    }
}
