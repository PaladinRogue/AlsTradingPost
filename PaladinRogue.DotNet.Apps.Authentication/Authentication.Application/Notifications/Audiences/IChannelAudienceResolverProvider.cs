using PaladinRogue.Authentication.Domain.NotificationTypes;

namespace PaladinRogue.Authentication.Application.Notifications.Audiences
{
    public interface IChannelAudienceResolverProvider
    {
        IChannelAudienceResolver GetByType(ChannelType channelType, string notificationType);
    }
}