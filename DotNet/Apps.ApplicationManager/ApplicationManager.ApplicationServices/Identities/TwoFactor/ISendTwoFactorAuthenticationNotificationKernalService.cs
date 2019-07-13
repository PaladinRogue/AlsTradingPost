using System.Threading.Tasks;

namespace ApplicationManager.ApplicationServices.Identities.TwoFactor
{
    public interface ISendTwoFactorAuthenticationNotificationKernalService
    {
        Task SendAsync(SendTwoFactorAuthenticationNotificationAdto sendTwoFactorAuthenticationNotificationAdto);
    }
}