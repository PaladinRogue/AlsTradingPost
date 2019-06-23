using System;
using System.Collections.Generic;
using ApplicationManager.Domain.NotificationTypes;

namespace ApplicationManager.ApplicationServices.Notifications.Audiences
{
    public interface IChannelAudienceResolver
    {
        ChannelType ChannelType { get; }

        IEnumerable<string> NotificationTypes { get; }

        IEnumerable<string> GetAudience(Guid identifier);
    }
}