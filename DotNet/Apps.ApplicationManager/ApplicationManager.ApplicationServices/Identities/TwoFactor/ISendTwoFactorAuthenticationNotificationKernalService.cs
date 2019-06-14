namespace ApplicationManager.ApplicationServices.Identities.TwoFactor
{
    public interface ISendTwoFactorAuthenticationNotificationKernalService
    {
        void Send(SendTwoFactorAuthenticationNotificationAdto sendTwoFactorAuthenticationNotificationAdto);
    }
}