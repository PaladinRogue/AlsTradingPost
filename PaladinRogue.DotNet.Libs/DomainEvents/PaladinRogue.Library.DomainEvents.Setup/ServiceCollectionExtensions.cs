using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Core.Domain.DomainEvents;
using PaladinRogue.Library.Core.Domain.DomainEvents.Interfaces;
using PaladinRogue.Library.DomainEvents.Broker;

namespace PaladinRogue.Library.DomainEvents.Setup
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