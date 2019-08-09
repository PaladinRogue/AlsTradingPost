using System.Collections.Generic;
using Authentication.Domain.Identities;
using Authentication.Domain.NotificationTypes;

namespace Authentication.Application.Identities.TwoFactor
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