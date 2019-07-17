using System;
using System.Threading.Tasks;
using Common.ApplicationServices.Caching;
using Common.Setup.Infrastructure.Authorisation;
using LazyCache;

namespace ApplicationManager.Setup.Infrastructure.Caching
{
    public class IdentityCacheService : ICacheService
    {
        private readonly IAppCache _appCache;

        private readonly ICurrentIdentityProvider _currentIdentityProvider;

        public IdentityCacheService(
            IAppCache appCache,
            ICurrentIdentityProvider currentIdentityProvider)
        {
            _appCache = appCache;
            _currentIdentityProvider = currentIdentityProvider;
        }

        public Task AddAsync<TKey, TValue>(
            TKey cacheKey,
            TValue item) where TKey : CacheKey<TValue>
        {
            _appCache.Add(CreateKey<TKey, TValue>(cacheKey), item);

            return Task.CompletedTask;
        }

        public Task<TValue> GetAsync<TKey, TValue>(TKey cacheKey) where TKey : CacheKey<TValue>
        {
            return _appCache.GetAsync<TValue>(CreateKey<TKey, TValue>(cacheKey));
        }

        public Task<TValue> GetOrAddAsync<TKey, TValue>(
            TKey cacheKey,
            Func<Task<TValue>> addItemFactory) where TKey : CacheKey<TValue>
        {
            return _appCache.GetOrAddAsync(CreateKey<TKey, TValue>(cacheKey), addItemFactory);
        }

        public Task RemoveAsync<TKey, TValue>(TKey cacheKey) where TKey : CacheKey<TValue>
        {
            _appCache.Remove(CreateKey<TKey, TValue>(cacheKey));

            return Task.CompletedTask;
        }

        private IdentityCacheKey<TKey, TValue> CreateKey<TKey, TValue>(TKey key) where TKey : CacheKey<TValue>
        {
            return new IdentityCacheKey<TKey, TValue>(key, _currentIdentityProvider.Id);
        }
    }
}