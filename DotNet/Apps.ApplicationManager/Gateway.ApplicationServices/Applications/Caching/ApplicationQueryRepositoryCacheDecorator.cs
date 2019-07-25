using System.Threading.Tasks;
using Common.ApplicationServices.Caching;
using Gateway.Domain.Applications;
using Gateway.Domain.Applications.Persistence;

namespace Gateway.ApplicationServices.Applications.Caching
{
    public class ApplicationQueryRepositoryCacheDecorator : CacheDecorator<string, ApplicationCacheKey, Application>, IApplicationQueryRepository
    {
        private readonly IApplicationQueryRepository _queryRepository;

        private readonly ICacheService _cacheService;

        public ApplicationQueryRepositoryCacheDecorator(
            IApplicationQueryRepository queryRepository,
            ICacheService cacheService) : base(cacheService)
        {
            _queryRepository = queryRepository;
            _cacheService = cacheService;
        }

        public Task<Application> GetByNameAsync(string name)
        {
            return _cacheService.GetOrAddAsync(new ApplicationCacheKey(name), () => _queryRepository.GetByNameAsync(name));
        }

        protected override ApplicationCacheKey CreateCacheKey(string key)
        {
            return new ApplicationCacheKey(key);
        }
    }
}