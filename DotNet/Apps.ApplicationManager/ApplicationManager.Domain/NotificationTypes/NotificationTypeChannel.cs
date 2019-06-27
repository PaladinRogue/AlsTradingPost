using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.Models;

namespace ApplicationManager.Domain.NotificationTypes
{
    public class NotificationTypeChannel : Entity, IAggregateMember
    {
        protected NotificationTypeChannel()
        {
        }
        
        public ChannelType ChannelType { get; protected set; }

        public virtual NotificationChannelTemplate NotificationChannelTemplate { get; protected set; }
        
        public virtual NotificationType NotificationType { get; protected set; }

        public IAggregateRoot AggregateRoot => NotificationType;
    }
}