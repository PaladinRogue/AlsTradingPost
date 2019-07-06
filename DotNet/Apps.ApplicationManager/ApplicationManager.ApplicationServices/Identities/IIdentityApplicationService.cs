using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Identities.Models;

namespace ApplicationManager.ApplicationServices.Identities
{
    public interface IIdentityApplicationService
    {
        Task<IdentityAdto> GetAsync(GetIdentityAdto getIdentityAdto);

        Task ResetPasswordAsync(ResetPasswordAdto resetPasswordAdto);

        Task ForgotPasswordAsync(ForgotPasswordAdto forgotPasswordAdto);

        Task ConfirmIdentityAsync(ConfirmIdentityAdto confirmIdentityAdto);

        Task<PasswordIdentityAdto> GetPasswordIdentityAsync(GetPasswordIdentityAdto getPasswordIdentityAdto);

        Task<PasswordIdentityAdto> ChangePasswordAsync(ChangePasswordAdto changePasswordAdto);

        Task<PasswordIdentityAdto> RegisterPasswordAsync(RegisterPasswordAdto registerPasswordAdto);

        Task<RefreshTokenIdentityAdto> CreateRefreshTokenAsync(CreateRefreshTokenAdto createRefreshTokenAdto);

        Task ResendConfirmIdentityAsync(ResendConfirmIdentityAdto resendConfirmIdentityAdto);

        Task LogoutAsync(LogoutAdto logoutAdto);
    }
}