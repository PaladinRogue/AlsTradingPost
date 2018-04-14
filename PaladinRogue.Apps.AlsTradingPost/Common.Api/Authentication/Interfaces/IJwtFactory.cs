using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.Api.Authentication.Interfaces
{
    public interface IJwtFactory
    {
        Task<T> GenerateJwt<T>(ClaimsIdentity identity) where T : IJwtResource;
    }
}
