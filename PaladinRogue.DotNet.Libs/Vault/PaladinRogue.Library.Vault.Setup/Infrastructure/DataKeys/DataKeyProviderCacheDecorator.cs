using System.Threading.Tasks;
using PaladinRogue.Library.Core.Application.Caching;
using PaladinRogue.Library.Core.Domain.DataProtectors;
using PaladinRogue.Library.Vault.Application.Caching;

namespace PaladinRogue.Library.Vault.Setup.Infrastructure.DataKeys
{
    public class DataKeyProviderCacheDecorator : CacheDecorator<string, DataKeyCacheKey, DataKey>, IDataKeyProvider
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

        public Task<DataKey> GetAsync(string name)
        {
            return _cacheService.GetOrAddAsync(new DataKeyCacheKey(name), () => _dataKeyProvider.GetAsync(name));
        }

        protected override DataKeyCacheKey CreateCacheKey(string name)
        {
            return  new DataKeyCacheKey(name);
        }
    }
}