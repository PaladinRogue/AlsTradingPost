using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using DomainEvent.Broker;
using DomainEvent.Broker.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup
{
    public static class EventRegistration
    {
	    public static void RegisterEventHandling(IServiceCollection services)
	    {
	        services.AddSingleton<IDomainEventBusSubscriptionsManager, InMemoryDomainEventBusSubscriptionsManager>();
            services.AddSingleton<IDomainEventBus, DomainEventBus>();
            services.AddSingleton<IDomainEventHandlerFactory, DomainEventHandlerFactory>();

	        PendingDomainEventDirector pendingDomainEventDirector = new PendingDomainEventDirector();
	        services.AddScoped<IPendingDomainEventContainer>(sp => pendingDomainEventDirector);
	        services.AddScoped<IPendingDomainEventProvider>(sp => pendingDomainEventDirector);
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        }
    }
}
