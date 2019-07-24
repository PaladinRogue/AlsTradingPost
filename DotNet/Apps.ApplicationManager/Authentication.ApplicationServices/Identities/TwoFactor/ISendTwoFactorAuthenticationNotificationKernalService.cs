using System.Threading.Tasks;

namespace Authentication.ApplicationServices.Identities.TwoFactor
{
    public interface ISendTwoFactorAuthenticationNotificationKernalService
    {
        Task SendAsync(SendTwoFactorAuthenticationNotificationAdto sendTwoFactorAuthenticationNotificationAdto);
    }
}