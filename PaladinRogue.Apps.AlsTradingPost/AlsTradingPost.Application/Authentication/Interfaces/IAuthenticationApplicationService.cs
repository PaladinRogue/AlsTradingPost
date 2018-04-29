using System.Threading.Tasks;
using AlsTradingPost.Application.Authentication.Models;
using Common.Application.Authentication;

namespace AlsTradingPost.Application.Authentication.Interfaces
{
    public interface IAuthenticationApplicationService
    {
        Task<JwtAdto> LoginAsync(LoginAdto loginAdto);
    }
}
