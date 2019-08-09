using System;
using Authentication.Domain.Identities;

namespace Authentication.Application.Identities.TwoFactor
{
    public class SendTwoFactorAuthenticationNotificationAdto
    {
        public Guid IdentityId { get; set; }

        public string Token { get; set; }

        public TwoFactorAuthenticationType TwoFactorAuthenticationType { get; set; }
    }
}