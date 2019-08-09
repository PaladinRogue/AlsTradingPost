using System.Threading.Tasks;
using Authentication.Application.Identities.Models;

namespace Authentication.Application.Identities
{
    public interface IIdentityApplicationService
    {
        Task<IdentityAdto> GetAsync(GetIdentityAdto getIdentityAdto);

        Task ResetPasswordAsync(ResetPasswordAdto resetPasswordAdto);

        Task ForgotPasswordAsync(ForgotPasswordAdto forgotPasswordAdto);

        Task ConfirmIdentityAsync(ConfirmIdentityAdto confirmIdentityAdto);

        Task<PasswordAdto> ChangePasswordAsync(ChangePasswordAdto changePasswordAdto);

        Task<PasswordAdto> RegisterPasswordAsync(RegisterPasswordAdto registerPasswordAdto);

        Task<RefreshTokenIdentityAdto> CreateRefreshTokenAsync(CreateRefreshTokenAdto createRefreshTokenAdto);

        Task ResendConfirmIdentityAsync(ResendConfirmIdentityAdto resendConfirmIdentityAdto);

        Task LogoutAsync(LogoutAdto logoutAdto);
    }
}