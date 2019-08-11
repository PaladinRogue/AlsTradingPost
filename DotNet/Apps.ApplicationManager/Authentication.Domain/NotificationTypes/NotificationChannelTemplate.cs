using PaladinRogue.Libray.Core.Domain.Aggregates;
using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Authentication.Domain.NotificationTypes
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