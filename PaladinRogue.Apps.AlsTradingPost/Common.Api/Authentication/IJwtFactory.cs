using System.Security.Claims;
using System.Threading.Tasks;
using Common.Api.Resource.Interfaces;

namespace Common.Api.Authentication
{
    public interface IJwtFactory
    {
        Task<T> GenerateJwt<T>(ClaimsIdentity identity) where T : IJwtResource;
    }
}
