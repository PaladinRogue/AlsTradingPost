using System.Threading.Tasks;

namespace Authentication.ApplicationServices.Identities.CreateAdmin
{
    public interface ICreateAdminAuthenticationIdentityKernalService
    {
        Task CreateAsync(CreateAdminAuthenticationIdentityAdto createAdminAuthenticationIdentityAdto);
    }
}
