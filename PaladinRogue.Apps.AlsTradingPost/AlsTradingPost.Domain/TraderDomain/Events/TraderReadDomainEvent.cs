using AlsTradingPost.Domain.Models;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.TraderDomain.Events
{
    public class TraderReadDomainEvent : IAuditedEvent
    {
        private TraderReadDomainEvent(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; set; }

        public static TraderReadDomainEvent Create(Trader trader)
        {
            return new TraderReadDomainEvent(trader);
        }
    }
}
