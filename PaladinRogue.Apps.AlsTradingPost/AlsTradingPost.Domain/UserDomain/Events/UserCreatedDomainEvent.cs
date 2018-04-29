using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.UserDomain.Events
{
    public class UserCreatedDomainEvent : IAuditedEvent
    {
        private UserCreatedDomainEvent(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; set; }

        public static UserCreatedDomainEvent Create(Domain.Models.User user)
        {
            return new UserCreatedDomainEvent(user);
        }
    }
}
