using Authentication.ApplicationServices.Identities.TwoFactor;
using Authentication.ApplicationServices.Users.UpdateClaim;
using Authentication.Domain.Identities.Events;
using Authentication.Domain.Users.Events;
using Common.Domain.DomainEvents.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Setup.Infrastructure.DomainEvents
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainEventSubscribers(this IServiceCollection services)
        {
            return services
                .AddScoped<IDomainEventSubscriber<TwoFactorAuthenticationIdentityCreatedDomainEvent>,
                    SendNotificationWhenTwoFactorAuthenticationIdentityCreatedDomainEventSubscriber>()
                .AddScoped<IDomainEventSubscriber<UserCreatedDomainEvent>, UpdateClaimsWhenUserCreatedDomainEventSubscriber>();
        }
    }
}
