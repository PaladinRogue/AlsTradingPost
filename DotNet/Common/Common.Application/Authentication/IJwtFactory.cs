using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.Application.Authentication
{
    public interface IJwtFactory
    {
        Task<T> GenerateJwt<T>(ClaimsIdentity identity, Guid sessionId) where T : IJwtAdto;
    }
}
