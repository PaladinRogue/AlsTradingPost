using System.Threading.Tasks;

namespace Authentication.Application.Identities.CreateAdmin
{
    public interface ICreateAdminAuthenticationIdentityKernalService
    {
        Task CreateAsync(CreateAdminAuthenticationIdentityAdto createAdminAuthenticationIdentityAdto);
    }
}
