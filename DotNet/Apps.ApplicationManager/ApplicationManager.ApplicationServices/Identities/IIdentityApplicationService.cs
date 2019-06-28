using ApplicationManager.ApplicationServices.Identities.Models;

namespace ApplicationManager.ApplicationServices.Identities
{
    public interface IIdentityApplicationService
    {
        IdentityAdto Get(GetIdentityAdto getIdentityAdto);

        void ResetPassword(ResetPasswordAdto resetPasswordAdto);

        void ForgotPassword(ForgotPasswordAdto forgotPasswordAdto);

        void ConfirmIdentity(ConfirmIdentityAdto confirmIdentityAdto);

        PasswordIdentityAdto GetPasswordIdentity(GetPasswordIdentityAdto getPasswordIdentityAdto);

        PasswordIdentityAdto ChangePassword(ChangePasswordAdto changePasswordAdto);

        PasswordIdentityAdto RegisterPassword(RegisterPasswordAdto registerPasswordAdto);

        RefreshTokenIdentityAdto CreateRefreshToken(CreateRefreshTokenAdto createRefreshTokenAdto);

        void Logout(LogoutAdto logoutAdto);
    }
}