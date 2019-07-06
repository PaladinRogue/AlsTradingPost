using System.Threading.Tasks;

namespace ApplicationManager.ApplicationServices.Identities.CreateAdmin
{
    public interface ICreateAdminAuthenticationIdentityKernalService
    {
        Task CreateAsync(CreateAdminAuthenticationIdentityAdto createAdminAuthenticationIdentityAdto);
    }
}
