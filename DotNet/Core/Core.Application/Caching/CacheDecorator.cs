using System.Threading.Tasks;

namespace PaladinRogue.Libray.Core.Application.Caching
{
    public abstract class CacheDecorator<TKey, TCacheKey, TCacheValue> : ICacheDecorator<TKey, TCacheValue> where TCacheKey : CacheKey<TCacheValue>
    {
        private readonly ICacheService _cacheService;

        protected CacheDecorator(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public Task AddAsync(TKey key, TCacheValue cacheValue)
        {
            return _cacheService.AddAsync(CreateCacheKey(key), cacheValue);
        }

        public async Task UpdateAsync(TKey key, TCacheValue cacheValue)
        {
            TCacheKey cacheKey = CreateCacheKey(key);

            await _cacheService.RemoveAsync<TCacheKey, TCacheValue>(cacheKey);
            await _cacheService.AddAsync(cacheKey, cacheValue);
        }

        public Task RemoveAsync(TKey key)
        {
            return _cacheService.RemoveAsync<TCacheKey, TCacheValue>(CreateCacheKey(key));
        }

        protected abstract TCacheKey CreateCacheKey(TKey key);
    }
}