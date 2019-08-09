using System.Threading.Tasks;
using Authentication.Application.Authentication.Models;
using Common.Application.Authentication;

namespace Authentication.Application.Authentication
{
    public interface IAuthenticationApplicationService
    {
        Task<JwtAdto> PasswordAsync(PasswordAdto passwordAdto);

        Task<JwtAdto> RefreshTokenAsync(RefreshTokenAdto refreshTokenAdto);

        Task<JwtAdto> GoogleAsync(ClientCredentialAdto clientCredentialAdto);

        Task<JwtAdto> FacebookAsync(ClientCredentialAdto clientCredentialAdto);
    }
}