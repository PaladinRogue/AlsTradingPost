using System.Collections.Generic;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.NotificationTypes
{
    public class NotificationType : VersionedEntity, IAggregateRoot
    {
        protected NotificationType()
        {
        }

        private  readonly ISet<NotificationTypeChannel> _notificationTypeChannels = new HashSet<NotificationTypeChannel>();

        public string Type { get; protected set; }
        
        public virtual IEnumerable<NotificationTypeChannel> NotificationTypeChannels => _notificationTypeChannels;
    }
}
