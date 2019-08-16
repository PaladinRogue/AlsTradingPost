using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PaladinRogue.Library.Core.Application.Authentication
{
    public interface IJwtFactory
    {
        Task<T> GenerateJwtAsync<T>(ClaimsIdentity identity, Guid sessionId) where T : IJwtAdto;
    }
}
