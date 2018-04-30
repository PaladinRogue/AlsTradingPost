using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.UserDomain.Events
{
    public class UserUpdatedDomainEvent : IAuditedEvent
    {
        private UserUpdatedDomainEvent(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; set; }

        public static UserUpdatedDomainEvent Create(Domain.Models.User user)
        {
            return new UserUpdatedDomainEvent(user);
        }
    }
}
