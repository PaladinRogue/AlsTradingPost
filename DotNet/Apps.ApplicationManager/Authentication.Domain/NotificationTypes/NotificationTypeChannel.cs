using PaladinRogue.Libray.Core.Domain.Aggregates;
using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Authentication.Domain.NotificationTypes
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