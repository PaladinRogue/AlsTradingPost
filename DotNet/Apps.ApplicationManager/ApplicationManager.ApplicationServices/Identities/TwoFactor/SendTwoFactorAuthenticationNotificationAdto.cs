using System;
using ApplicationManager.Domain.Identities;

namespace ApplicationManager.ApplicationServices.Identities.TwoFactor
{
    public class SendTwoFactorAuthenticationNotificationAdto
    {
        public Guid IdentityId { get; set; }

        public string Token { get; set; }

        public TwoFactorAuthenticationType TwoFactorAuthenticationType { get; set; }
    }
}