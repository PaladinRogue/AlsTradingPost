using System;

namespace ApplicationManager.ApplicationServices.Notifications
{
    public class SendTwoFactorAuthenticationNotificationAdto
    {
        public Guid IdentityId { get; set; }
        
        public string Token { get; set; }
    }
}