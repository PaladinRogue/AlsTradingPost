using ApplicationManager.ApplicationServices.Identities.Handlers;
using ApplicationManager.Domain.Applications.Events;
using Common.Domain.DomainEvents.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationManager.Setup
{
    public static class EventRegistration
    {
        public static void RegisterHandlers(IServiceCollection services)
        {
            services.AddScoped<IDomainEventHandler<ApplicationCreatedDomainEvent>, CreateAdminIdentityWhenApplicationCreatedDomainEventHandler>();
        }
    }
}
