using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Core.Domain.DomainEvents;
using PaladinRogue.Libray.Core.Domain.DomainEvents.Interfaces;
using PaladinRogue.Libray.DomainEvents.Broker;

namespace PaladinRogue.Libray.DomainEvents.Setup
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