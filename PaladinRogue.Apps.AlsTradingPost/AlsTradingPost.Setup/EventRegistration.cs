using AlsTradingPost.Domain.EventHandlers;
using Common.Domain.DomainEvents.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AlsTradingPost.Setup
{
    public static class EventRegistration
    {
	    public static void RegisterHandlers(IServiceCollection services)
	    {
		    Common.Setup.EventRegistration.RegisterEventHandling(services);

	        services.AddScoped<IDomainEventHandler, AuditedEventHandler>();
        }
	}
}
