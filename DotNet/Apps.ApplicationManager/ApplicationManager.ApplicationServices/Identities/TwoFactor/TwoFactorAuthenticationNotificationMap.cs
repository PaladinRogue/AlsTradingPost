using System.Collections.Generic;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.NotificationTypes;

namespace ApplicationManager.ApplicationServices.Identities.TwoFactor
{
    public static class TwoFactorAuthenticationNotificationMap
    {
        private static readonly IReadOnlyDictionary<TwoFactorAuthenticationType, string> Map = new Dictionary<TwoFactorAuthenticationType, string>
        {
            {TwoFactorAuthenticationType.ConfirmIdentity, NotificationNames.ConfirmIdentity},
            {TwoFactorAuthenticationType.ForgotPassword, NotificationNames.ForgotPassword}
        };

        public static string ForType(TwoFactorAuthenticationType twoFactorAuthenticationType)
        {
            return Map[twoFactorAuthenticationType];
        }
    }
}