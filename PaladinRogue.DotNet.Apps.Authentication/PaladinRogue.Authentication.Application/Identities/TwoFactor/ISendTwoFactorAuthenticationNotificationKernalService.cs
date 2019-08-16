using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Application.Identities.TwoFactor
{
    public interface ISendTwoFactorAuthenticationNotificationKernalService
    {
        Task SendAsync(SendTwoFactorAuthenticationNotificationAdto sendTwoFactorAuthenticationNotificationAdto);
    }
}