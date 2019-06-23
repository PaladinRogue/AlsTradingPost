using System;
using System.Collections.Generic;
using ApplicationManager.Domain.Identities.Projections;
using ApplicationManager.Domain.Identities.Queries;
using ApplicationManager.Domain.NotificationTypes;

namespace ApplicationManager.ApplicationServices.Notifications.Audiences
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

        public IEnumerable<string> NotificationTypes { get; } = new List<string>
        {
            NotificationNames.ConfirmIdentity,
            NotificationNames.ForgotPassword
        };

        public IEnumerable<string> GetAudience(Guid identifier)
        {
            TwoFactorAuthenticationIdentityProjection twoFactorAuthenticationIdentityProjection = _getTwoFactorAuthenticationIdentityByIdentityQuery.Run(identifier);

            return new List<string>
            {
                twoFactorAuthenticationIdentityProjection.EmailAddress
            };
        }
    }
}