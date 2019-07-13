using ApplicationManager.ApplicationServices.Identities.TwoFactor;
using ApplicationManager.Domain.Identities.Events;
using Common.Domain.DomainEvents.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationManager.Setup.Infrastructure.DomainEvents
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainEventHandlers(this IServiceCollection services)
        {
            return services
                .AddScoped<IDomainEventHandler<TwoFactorAuthenticationIdentityCreatedDomainEvent>,
                    SendNotificationWhenTwoFactorAuthenticationIdentityCreatedDomainEventHandler>();
        }
    }
}
