using System;
using System.Collections.Generic;
using ApplicationManager.Domain.NotificationTypes;

namespace ApplicationManager.ApplicationServices.Notifications.Audience
{
    public interface IChannelAudienceResolver
    {
        ChannelType ChannelType { get; }
        
        string NotificationType { get; }
        
        IEnumerable<string> GetAudience(Guid identifier);
    }
}