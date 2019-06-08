namespace ApplicationManager.ApplicationServices.Notifications
{
    public interface ISendTwoFactorAuthenticationNotificationKernalService
    {
        void Send(SendTwoFactorAuthenticationNotificationAdto sendTwoFactorAuthenticationNotificationAdto);
    }
}