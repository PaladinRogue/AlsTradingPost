using System.Threading.Tasks;
using PaladinRogue.Authentication.Application.Claims;
using PaladinRogue.Authentication.Domain.Users.Events;
using PaladinRogue.Libray.Authorisation.Application.ApplicationServices;
using PaladinRogue.Libray.Core.Domain.DomainEvents.Interfaces;

namespace PaladinRogue.Authentication.Application.Users.UpdateClaim
{
    public class UpdateClaimsWhenUserCreatedDomainEventSubscriber : IDomainEventSubscriber<UserCreatedDomainEvent>
    {
        private readonly IClaimsApplicationKernalService _claimsApplicationKernalService;

        public UpdateClaimsWhenUserCreatedDomainEventSubscriber(
            IClaimsApplicationKernalService claimsApplicationKernalService)
        {
            _claimsApplicationKernalService = claimsApplicationKernalService;
        }

        public Task ExecuteAsync(UserCreatedDomainEvent domainEvent)
        {
            return _claimsApplicationKernalService.AddAsync(new AddClaimAdto
            {
                IdentityId = domainEvent.User.Identity.Id,
                Type = JwtClaimIdentifiers.User,
                Value = domainEvent.User.Id.ToString()
            });
        }
    }
}