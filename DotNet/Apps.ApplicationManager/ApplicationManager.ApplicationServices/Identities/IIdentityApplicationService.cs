using ApplicationManager.ApplicationServices.Identities.Models;

namespace ApplicationManager.ApplicationServices.Identities
{
    public interface IIdentityApplicationService
    {
        IdentityAdto Get(GetIdentityAdto getIdentityAdto);

        PasswordIdentityAdto CreateConfirmedPasswordIdentity(CreateConfirmedPasswordIdentityAdto createConfirmedPasswordIdentityAdto);

        PasswordIdentityAdto GetPasswordIdentity(GetPasswordIdentityAdto getPasswordIdentityAdto);

        PasswordIdentityAdto ChangePassword(ChangePasswordAdto changePasswordAdto);

        PasswordIdentityAdto RegisterPassword(RegisterPasswordAdto registerPasswordAdto);
    }
}