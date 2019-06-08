using ApplicationManager.ApplicationServices.Identities.Models;

namespace ApplicationManager.ApplicationServices.Identities.Interfaces
{
    public interface ICreateAdminAuthenticationIdentityKernalService
    {
        void Create(CreateAdminAuthenticationIdentityAdto createAdminAuthenticationIdentityAdto);
    }
}
