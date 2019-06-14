using ApplicationManager.Domain.NotificationTypes;

namespace ApplicationManager.ApplicationServices.Notifications.Audiences
{
    public interface IChannelAudienceResolverProvider
    {
        IChannelAudienceResolver GetByType(ChannelType channelType, string notificationType);
    }
}