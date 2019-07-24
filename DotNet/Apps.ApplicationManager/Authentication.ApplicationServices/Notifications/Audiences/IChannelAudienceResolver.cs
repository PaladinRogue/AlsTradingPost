using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Domain.NotificationTypes;

namespace Authentication.ApplicationServices.Notifications.Audiences
{
    public interface IChannelAudienceResolver
    {
        ChannelType ChannelType { get; }

        IEnumerable<string> NotificationTypes { get; }

        Task<IEnumerable<string>> GetAudienceAsync(Guid identifier);
    }
}