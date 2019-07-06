using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationManager.Domain.NotificationTypes;

namespace ApplicationManager.ApplicationServices.Notifications.Audiences
{
    public interface IChannelAudienceResolver
    {
        ChannelType ChannelType { get; }

        IEnumerable<string> NotificationTypes { get; }

        Task<IEnumerable<string>> GetAudienceAsync(Guid identifier);
    }
}