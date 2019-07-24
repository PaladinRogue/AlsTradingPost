using Common.Domain.Aggregates;
using Common.Domain.Entities;

namespace Authentication.Domain.NotificationTypes
{
    public abstract class NotificationChannelTemplate: Entity, IAggregateMember
    {
        protected NotificationChannelTemplate()
        {
        }

        public virtual NotificationTypeChannel NotificationTypeChannel { get; protected set; }

        public IAggregateRoot AggregateRoot => NotificationTypeChannel.AggregateRoot;
    }
}