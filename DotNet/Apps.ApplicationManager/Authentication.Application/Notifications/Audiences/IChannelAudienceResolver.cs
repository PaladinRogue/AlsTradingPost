using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Authentication.Domain.NotificationTypes;

namespace PaladinRogue.Authentication.Application.Notifications.Audiences
{
    public interface IChannelAudienceResolver
    {
        ChannelType ChannelType { get; }

        IEnumerable<string> NotificationTypes { get; }

        Task<IEnumerable<string>> GetAudienceAsync(Guid identifier);
    }
}