using AlsTradingPost.Domain.Models;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.DomainEvents
{
    public class UserUpdatedDomainEvent : IAuditedEvent
    {
        private UserUpdatedDomainEvent(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; set; }

        public static UserUpdatedDomainEvent Create(User user)
        {
            return new UserUpdatedDomainEvent(user);
        }
    }
}
