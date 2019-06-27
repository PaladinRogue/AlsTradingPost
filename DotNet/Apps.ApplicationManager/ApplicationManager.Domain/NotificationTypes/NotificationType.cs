using System.Collections.Generic;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.Models;

namespace ApplicationManager.Domain.NotificationTypes
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
