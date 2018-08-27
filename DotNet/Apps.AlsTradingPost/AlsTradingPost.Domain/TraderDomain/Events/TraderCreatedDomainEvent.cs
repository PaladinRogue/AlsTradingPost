using AlsTradingPost.Domain.Models;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.TraderDomain.Events
{
    public class TraderCreatedDomainEvent : IAuditedEvent
    {
        private TraderCreatedDomainEvent(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; set; }

        public static TraderCreatedDomainEvent Create(Trader trader)
        {
            return new TraderCreatedDomainEvent(trader);
        }
    }
}
