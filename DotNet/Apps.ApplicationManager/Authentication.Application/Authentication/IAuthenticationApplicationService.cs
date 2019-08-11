using System.Threading.Tasks;
using PaladinRogue.Authentication.Application.Authentication.Models;
using PaladinRogue.Libray.Core.Application.Authentication;

namespace PaladinRogue.Authentication.Application.Authentication
{
    public interface IAuthenticationApplicationService
    {
        Task<JwtAdto> PasswordAsync(PasswordAdto passwordAdto);

        Task<JwtAdto> RefreshTokenAsync(RefreshTokenAdto refreshTokenAdto);

        Task<JwtAdto> GoogleAsync(ClientCredentialAdto clientCredentialAdto);

        Task<JwtAdto> FacebookAsync(ClientCredentialAdto clientCredentialAdto);
    }
}