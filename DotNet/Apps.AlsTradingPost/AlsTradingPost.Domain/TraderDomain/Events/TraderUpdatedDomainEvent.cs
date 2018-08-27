using AlsTradingPost.Domain.Models;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.TraderDomain.Events
{
    public class TraderUpdatedDomainEvent : IAuditedEvent
    {
        private TraderUpdatedDomainEvent(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; set; }

        public static TraderUpdatedDomainEvent Create(Trader trader)
        {
            return new TraderUpdatedDomainEvent(trader);
        }
    }
}
