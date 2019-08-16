using System.Threading.Tasks;
using PaladinRogue.Authentication.Application.Claims;
using PaladinRogue.Authentication.Domain.Users.Events;
using PaladinRogue.Authentication.Messages;
using PaladinRogue.Library.Core.Domain.DomainEvents.Interfaces;
using PaladinRogue.Library.Messaging.Common.Messages;

namespace PaladinRogue.Authentication.Application.Users.UpdateClaim
{
    public class UpdateClaimsWhenUserCreatedDomainEventSubscriber : IDomainEventSubscriber<UserCreatedDomainEvent>
    {
        public Task ExecuteAsync(UserCreatedDomainEvent domainEvent)
        {
            return Message.SendAsync(AddAuthorisationClaimMessage.Create(domainEvent.User.Identity.Id, JwtClaimIdentifiers.User, domainEvent.User.Id.ToString()));
        }
    }
}