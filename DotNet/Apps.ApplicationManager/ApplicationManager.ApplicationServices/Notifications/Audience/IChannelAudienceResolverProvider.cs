using ApplicationManager.Domain.NotificationTypes;

namespace ApplicationManager.ApplicationServices.Notifications.Audience
{
    public interface IChannelAudienceResolverProvider
    {
        IChannelAudienceResolver GetByType(ChannelType channelType, string notificationType);
    }
}