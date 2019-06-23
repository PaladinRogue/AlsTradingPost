using System;

namespace ApplicationManager.ApplicationServices.Identities.TwoFactor
{
    public class SendTwoFactorAuthenticationNotificationAdto
    {
        public Guid IdentityId { get; set; }

        public string Token { get; set; }

        public string TwoFactorAuthenticationType { get; set; }
    }
}