using System;
using AlsTradingPost.Domain.AuditDomain.Handlers;
using Common.Domain.DomainEvents.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AlsTradingPost.Setup
{
    public static class EventRegistration
    {
	    public static void RegisterHandlers(IServiceCollection services)
	    {
            services.AddScoped<IDomainEventHandler<IAuditedEvent>, AuditedEventHandler>();
        }

	    public static void AddHandlers(IServiceProvider serviceProvider)
	    {
	        foreach (IDomainEventHandler domainEventHandler in serviceProvider.GetServices(typeof(IDomainEventHandler<IAuditedEvent>)))
	        {
	            domainEventHandler.Register();
            }
	    }
	}
}
