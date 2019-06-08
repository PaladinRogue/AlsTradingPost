using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationManager.Domain.NotificationTypes;
using Common.Application.Exceptions;

namespace ApplicationManager.ApplicationServices.Notifications.Audience
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
                    r.ChannelType == channelType && r.NotificationType == notificationType);

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