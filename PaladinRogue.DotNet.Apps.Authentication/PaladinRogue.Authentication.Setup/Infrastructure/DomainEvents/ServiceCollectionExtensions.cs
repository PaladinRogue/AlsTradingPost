using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Authentication.Application.Identities.TwoFactor;
using PaladinRogue.Authentication.Application.Users.UpdateClaim;
using PaladinRogue.Authentication.Domain.Identities.Events;
using PaladinRogue.Authentication.Domain.Users.Events;
using PaladinRogue.Library.Core.Domain.DomainEvents.Interfaces;

namespace PaladinRogue.Authentication.Setup.Infrastructure.DomainEvents
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
