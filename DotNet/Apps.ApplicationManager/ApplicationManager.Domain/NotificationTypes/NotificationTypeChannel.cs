using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.NotificationTypes
{
    public class NotificationTypeChannel : Entity, IAggregateMember
    {
        protected NotificationTypeChannel()
        {
        }
        
        public ChannelType ChannelType { get; protected set; }
        
        [Required]
        public virtual NotificationChannelTemplate NotificationChannelTemplate { get; protected set; }
        
        public virtual NotificationType NotificationType { get; protected set; }

        public IAggregateRoot AggregateRoot => NotificationType;
    }
}