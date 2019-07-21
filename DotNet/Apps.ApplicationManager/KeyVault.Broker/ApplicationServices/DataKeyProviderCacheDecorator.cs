using System;
using System.Threading.Tasks;
using Common.ApplicationServices.Caching;
using KeyVault.Broker.Domain;

namespace KeyVault.Broker.ApplicationServices
{
    public class DataKeyProviderCacheDecorator<T> : CacheDecorator<T, DataKeyCacheKey<T>, DataKey<T>>, IDataKeyProvider where T : struct, Enum
    {
        private readonly IDataKeyProvider _dataKeyProvider;

        private readonly ICacheService _cacheService;

        public DataKeyProviderCacheDecorator(
            IDataKeyProvider dataKeyProvider,
            ICacheService cacheService) : base(cacheService)
        {
            _dataKeyProvider = dataKeyProvider;
            _cacheService = cacheService;
        }

        public Task<DataKey<TType>> GetAsync<TType>(TType type) where TType : struct, Enum
        {
            return _cacheService.GetOrAddAsync(new DataKeyCacheKey<TType>(type), () => _dataKeyProvider.GetAsync(type));
        }

        protected override DataKeyCacheKey<T> CreateCacheKey(T key)
        {
            return  new DataKeyCacheKey<T>(key);
        }
    }
}