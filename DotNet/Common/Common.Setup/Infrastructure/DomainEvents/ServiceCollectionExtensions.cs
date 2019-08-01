using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using DomainEvent.Broker;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup.Infrastructure.DomainEvents
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDomainEvents(this IServiceCollection services)
        {
            return services
                .AddSingleton<IDomainEventBus, DomainEventBus>()
                .AddSingleton<IDomainEventSubscriberResolver, DomainEventSubscriberResolver>()
                .AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        }
    }
}