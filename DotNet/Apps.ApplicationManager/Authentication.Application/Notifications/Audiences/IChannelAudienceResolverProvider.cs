using Authentication.Domain.NotificationTypes;

namespace Authentication.Application.Notifications.Audiences
{
    public interface IChannelAudienceResolverProvider
    {
        IChannelAudienceResolver GetByType(ChannelType channelType, string notificationType);
    }
}