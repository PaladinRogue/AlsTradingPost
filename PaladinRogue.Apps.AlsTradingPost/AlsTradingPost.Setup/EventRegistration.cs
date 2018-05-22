using AlsTradingPost.Domain.AuditDomain.Handlers;
using Common.Domain.DomainEvents.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AlsTradingPost.Setup
{
    public static class EventRegistration
    {
	    public static void RegisterHandlers(IServiceCollection services)
	    {
            services.AddTransient<IDomainEventHandler<IAuditedEvent>, AuditedEventHandler>();
        }
	}
}
