using System.Threading.Tasks;
using Authentication.ApplicationServices.Claims;
using Authentication.Domain.Users.Events;
using Common.Authorisation.ApplicationServices;
using Common.Domain.DomainEvents.Interfaces;

namespace Authentication.ApplicationServices.Users.UpdateClaim
{
    public class UpdateClaimsWhenUserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        private readonly IClaimsApplicationKernalService _claimsApplicationKernalService;

        public UpdateClaimsWhenUserCreatedDomainEventHandler(
            IClaimsApplicationKernalService claimsApplicationKernalService)
        {
            _claimsApplicationKernalService = claimsApplicationKernalService;
        }

        public Task HandleAsync(UserCreatedDomainEvent domainEvent)
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