using Common.Domain.DomainEvents;
using Common.Domain.DomainEvents.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Setup
{
    public static class EventRegistration
    {
	    public static void RegisterEventHandling(IServiceCollection services)
	    {
		    services.AddScoped<IDomainEventHandlerResolver, DomainEventHandlerResolver>();
		    services.AddScoped<IDomainEventFactory, DomainEventFactory>();
			services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
	    }
    }
}
