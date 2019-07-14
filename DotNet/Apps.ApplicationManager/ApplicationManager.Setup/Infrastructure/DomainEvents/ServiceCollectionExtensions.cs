using ApplicationManager.ApplicationServices.Identities.TwoFactor;
using ApplicationManager.ApplicationServices.Users.UpdateClaim;
using ApplicationManager.Domain.Identities.Events;
using ApplicationManager.Domain.Users.Events;
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
                    SendNotificationWhenTwoFactorAuthenticationIdentityCreatedDomainEventHandler>()
                .AddScoped<IDomainEventHandler<UserCreatedDomainEvent>, UpdateClaimsWhenUserCreatedDomainEventHandler>();
        }
    }
}
