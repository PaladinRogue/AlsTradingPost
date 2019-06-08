using ApplicationManager.ApplicationServices.NotificationTypes.Handlers;
using ApplicationManager.Domain.Identities.Events;
using Common.Domain.DomainEvents.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationManager.Setup
{
    public static class EventRegistration
    {
        public static void RegisterHandlers(IServiceCollection services)
        {
            services
                .AddScoped<IDomainEventHandler<TwoFactorAuthenticationIdentityCreatedDomainEvent>,
                    SendNotificationWhenTwoFactorAuthenticationIdentityCreatedDomainEventHandler>();
        }
    }
}
