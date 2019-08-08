using System.Threading.Tasks;
using Authentication.ApplicationServices.Claims;
using Authentication.Domain.Users.Events;
using Authorisation.Application.ApplicationServices;
using Common.Domain.DomainEvents.Interfaces;

namespace Authentication.ApplicationServices.Users.UpdateClaim
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