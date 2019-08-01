using System.Threading.Tasks;
using Authentication.ApplicationServices.Authentication.Models;
using Common.ApplicationServices.Authentication;

namespace Authentication.ApplicationServices.Authentication
{
    public interface IAuthenticationApplicationService
    {
        Task<JwtAdto> PasswordAsync(PasswordAdto passwordAdto);

        Task<JwtAdto> RefreshTokenAsync(RefreshTokenAdto refreshTokenAdto);

        Task<JwtAdto> GoogleAsync(ClientCredentialAdto clientCredentialAdto);

        Task<JwtAdto> FacebookAsync(ClientCredentialAdto clientCredentialAdto);
    }
}