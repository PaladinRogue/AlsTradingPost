using System.Threading.Tasks;

namespace Authentication.Application.Identities.TwoFactor
{
    public interface ISendTwoFactorAuthenticationNotificationKernalService
    {
        Task SendAsync(SendTwoFactorAuthenticationNotificationAdto sendTwoFactorAuthenticationNotificationAdto);
    }
}