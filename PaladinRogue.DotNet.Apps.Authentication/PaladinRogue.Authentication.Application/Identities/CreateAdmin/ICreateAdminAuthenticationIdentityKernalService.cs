using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Application.Identities.CreateAdmin
{
    public interface ICreateAdminAuthenticationIdentityKernalService
    {
        Task CreateAsync(CreateAdminAuthenticationIdentityAdto createAdminAuthenticationIdentityAdto);
    }
}
