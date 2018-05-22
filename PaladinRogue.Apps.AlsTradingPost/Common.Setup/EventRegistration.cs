using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using DomainEvent.Broker;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup
{
    public static class EventRegistration
    {
	    public static void RegisterEventHandling(IServiceCollection services)
	    {
            services.AddSingleton<IDomainEventBus, DomainEventBus>();
            services.AddSingleton<IDomainEventHandlerResolver, DomainEventHandlerResolver>();
            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        }
    }
}
