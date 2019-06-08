using System;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.NotificationTypes
{
    public abstract class NotificationChannelTemplate: Entity, IAggregateMember
    {
        public abstract string Type { get; protected set; }
        
        public Guid NotificationTypeChannelId { get; protected set; }
        
        [Required]
        public virtual NotificationTypeChannel NotificationTypeChannel { get; protected set; }

        public IAggregateRoot AggregateRoot => NotificationTypeChannel.AggregateRoot;
    }
}