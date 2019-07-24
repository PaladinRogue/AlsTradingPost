using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Domain.Identities.Projections;
using Authentication.Domain.Identities.Queries;
using Authentication.Domain.NotificationTypes;

namespace Authentication.ApplicationServices.Notifications.Audiences
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

        public async Task<IEnumerable<string>> GetAudienceAsync(Guid identifier)
        {
            TwoFactorAuthenticationIdentityProjection twoFactorAuthenticationIdentityProjection = await _getTwoFactorAuthenticationIdentityByIdentityQuery.RunAsync(identifier);

            return new List<string>
            {
                twoFactorAuthenticationIdentityProjection.EmailAddress
            };
        }
    }
}