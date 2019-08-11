using System.Collections.Generic;
using PaladinRogue.Libray.Core.Domain.Aggregates;
using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Authentication.Domain.NotificationTypes
{
    public class NotificationType : VersionedEntity, IAggregateRoot
    {
        protected NotificationType()
        {
        }

        private readonly ISet<NotificationTypeChannel> _notificationTypeChannels = new HashSet<NotificationTypeChannel>();

        public string Type { get; protected set; }

        public virtual IEnumerable<NotificationTypeChannel> NotificationTypeChannels => _notificationTypeChannels;
    }
}
