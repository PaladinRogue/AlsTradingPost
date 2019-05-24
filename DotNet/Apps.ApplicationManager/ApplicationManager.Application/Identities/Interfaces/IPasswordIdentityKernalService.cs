using ApplicationManager.ApplicationServices.Identities.Models;

namespace ApplicationManager.ApplicationServices.Identities.Interfaces
{
    public interface IPasswordIdentityKernalService
    {
        void Create(CreatePasswordIdentityAdto createPasswordIdentityAdto);
    }
}
