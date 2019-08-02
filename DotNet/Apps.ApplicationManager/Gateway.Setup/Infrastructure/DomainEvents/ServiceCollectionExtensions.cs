using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Setup.Infrastructure.DomainEvents
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainEventSubscribers(this IServiceCollection services)
        {
            return services;
        }
    }
}
