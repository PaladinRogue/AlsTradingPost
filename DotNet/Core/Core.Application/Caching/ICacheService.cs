using System;
using System.Threading.Tasks;

namespace PaladinRogue.Libray.Core.Application.Caching
{
    public interface ICacheService
    {
        Task AddAsync<TCacheKey, TValue>(TCacheKey cacheKey, TValue item) where TCacheKey : CacheKey<TValue>;

        Task<TValue> GetAsync<TCacheKey, TValue>(TCacheKey cacheKey) where TCacheKey : CacheKey<TValue>;

        Task<TValue> GetOrAddAsync<TCacheKey, TValue>(TCacheKey cacheKey, Func<Task<TValue>> addItemFactory) where TCacheKey : CacheKey<TValue>;

        Task RemoveAsync<TCacheKey, TValue>(TCacheKey cacheKey) where TCacheKey : CacheKey<TValue>;
    }
}