using System.Threading.Tasks;
using Authentication.Application.Authentication.Models;
using Common.Application.Authentication;

namespace Authentication.Application.Authentication.Interfaces
{
    public interface IAuthenticationApplicationService
    {
        Task<ExtendedJwtAdto> LoginAsync(LoginAdto loginAdto);
        
        Task<JwtAdto> RefreshTokenAsync(RefreshTokenAdto refreshTokenAdto);
    }
}

