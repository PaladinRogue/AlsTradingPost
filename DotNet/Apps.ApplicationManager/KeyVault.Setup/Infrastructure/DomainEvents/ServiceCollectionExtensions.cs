using Microsoft.Extensions.DependencyInjection;

namespace KeyVault.Setup.Infrastructure.DomainEvents
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainEventHandlers(this IServiceCollection services)
        {
            return services;
        }
    }
}
