using ApplicationManager.ApplicationServices.Identities.Models;

namespace ApplicationManager.ApplicationServices.Identities.Interfaces
{
    public interface ITwoFactorAuthenticationIdentityKernalService
    {
        void Create(CreateTwoFactorAuthenticationIdentityAdto createTwoFactorAuthenticationIdentityAdto);
    }
}
