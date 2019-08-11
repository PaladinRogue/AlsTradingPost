using System;
using System.Threading.Tasks;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using NodaTime;
using PaladinRogue.Libray.Core.Application.Caching;

namespace PaladinRogue.Vault.Setup.Infrastructure.Caching
{
    public class VaultCacheService : ICacheService
    {
        private readonly IAppCache _appCache;

        private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions;

        public VaultCacheService(
            IAppCache appCache,
            IClock clock)
        {
            _appCache = appCache;

            _memoryCacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = clock.GetCurrentInstant().Plus(Duration.FromHours(1)).ToDateTimeOffset()
            };
        }

        public Task AddAsync<TKey, TValue>(
            TKey cacheKey,
            TValue item) where TKey : CacheKey<TValue>
        {
            _appCache.Add(cacheKey, item, _memoryCacheEntryOptions);

            return Task.CompletedTask;
        }

        public Task<TValue> GetAsync<TKey, TValue>(TKey cacheKey) where TKey : CacheKey<TValue>
        {
            return _appCache.GetAsync<TValue>(cacheKey);
        }

        public Task<TValue> GetOrAddAsync<TKey, TValue>(
            TKey cacheKey,
            Func<Task<TValue>> addItemFactory) where TKey : CacheKey<TValue>
        {
            return _appCache.GetOrAddAsync(cacheKey, addItemFactory, _memoryCacheEntryOptions);
        }

        public Task RemoveAsync<TKey, TValue>(TKey cacheKey) where TKey : CacheKey<TValue>
        {
            _appCache.Remove(cacheKey);

            return Task.CompletedTask;
        }
    }
}