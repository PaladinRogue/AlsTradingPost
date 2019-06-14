using ApplicationManager.ApplicationServices.Identities.Models;

namespace ApplicationManager.ApplicationServices.Identities.Admin
{
    public interface ICreateAdminAuthenticationIdentityKernalService
    {
        void Create(CreateAdminAuthenticationIdentityAdto createAdminAuthenticationIdentityAdto);
    }
}
