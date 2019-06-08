using System;
using System.Collections.Generic;
using ApplicationManager.ApplicationServices.Identities.Interfaces;
using ApplicationManager.Domain.NotificationTypes;

namespace ApplicationManager.ApplicationServices.Notifications.Audience
{
    public class TwoFactorAuthenticationEmailChannelResolver : IChannelAudienceResolver
    {
        private readonly IGetTwoFactorAuthenticationIdentityByIdentityQuery
            _getTwoFactorAuthenticationIdentityByIdentityQuery;

        public TwoFactorAuthenticationEmailChannelResolver(
            IGetTwoFactorAuthenticationIdentityByIdentityQuery getTwoFactorAuthenticationIdentityByIdentityQuery)
        {
            _getTwoFactorAuthenticationIdentityByIdentityQuery = getTwoFactorAuthenticationIdentityByIdentityQuery;
        }

        public ChannelType ChannelType => ChannelType.Email;
        
        public string NotificationType => Domain.NotificationTypes.NotificationTypes.EmailTwoFactorAuthentication;
        
        public IEnumerable<string> GetAudience(Guid identifier)
        {
            TwoFactorAuthenticationIdentityProjection twoFactorAuthenticationIdentityProjection = _getTwoFactorAuthenticationIdentityByIdentityQuery.Execute(identifier);

            return new List<string>
            {
                twoFactorAuthenticationIdentityProjection.EmailAddress
            };
        }
    }
}