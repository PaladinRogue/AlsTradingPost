using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Authentication.Models;
using Common.ApplicationServices.Authentication;

namespace ApplicationManager.ApplicationServices.Authentication
{
    public interface IAuthenticationApplicationService
    {
        Task<JwtAdto> PasswordAsync(PasswordAdto passwordAdto);

        Task<JwtAdto> RefreshTokenAsync(RefreshTokenAdto refreshTokenAdto);

        Task<JwtAdto> ClientCredentialAsync(ClientCredentialAdto clientCredentialAdto);
    }
}