using Authentication.Domain.NotificationTypes;

namespace Authentication.ApplicationServices.Notifications.Audiences
{
    public interface IChannelAudienceResolverProvider
    {
        IChannelAudienceResolver GetByType(ChannelType channelType, string notificationType);
    }
}