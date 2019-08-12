using System;
using System.Collections.Generic;
using System.Linq;
using PaladinRogue.Authentication.Domain.NotificationTypes;
using PaladinRogue.Library.Core.Application.Exceptions;

namespace PaladinRogue.Authentication.Application.Notifications.Audiences
{
    public class ChannelAudienceResolverProvider : IChannelAudienceResolverProvider
    {
        private readonly IEnumerable<IChannelAudienceResolver> _channelAudienceResolvers;

        public ChannelAudienceResolverProvider(IEnumerable<IChannelAudienceResolver> channelAudienceResolvers)
        {
            _channelAudienceResolvers = channelAudienceResolvers;
        }

        public IChannelAudienceResolver GetByType(ChannelType channelType, string notificationType)
        {
            try
            {
                IChannelAudienceResolver channelAudienceResolver = _channelAudienceResolvers.SingleOrDefault(r =>
                    r.ChannelType == channelType && r.NotificationTypes.Any(n => n == notificationType));

                if (channelAudienceResolver == null)
                {
                    throw new BusinessApplicationException(ExceptionType.Unknown, $"No audience resolver defined for Channel: {channelType} and NotificationType: {notificationType}");
                }

                return channelAudienceResolver;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}