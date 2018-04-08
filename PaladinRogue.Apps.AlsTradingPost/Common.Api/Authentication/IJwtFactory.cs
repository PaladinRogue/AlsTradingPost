using System;
using System.Threading.Tasks;
using Common.Api.Resource.Interfaces;

namespace Common.Api.Authentication
{
    public interface IJwtFactory
    {
        Task<T> GenerateJwt<T>(Guid id) where T : IJwtResource;
    }
}
