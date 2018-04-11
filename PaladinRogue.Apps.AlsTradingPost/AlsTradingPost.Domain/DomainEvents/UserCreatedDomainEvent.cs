using AlsTradingPost.Domain.Models;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.DomainEvents
{
    public class UserCreatedDomainEvent : IAuditedEvent
    {
        private UserCreatedDomainEvent(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; set; }

        public static UserCreatedDomainEvent Create(User user)
        {
            return new UserCreatedDomainEvent(user);
        }
    }
}
