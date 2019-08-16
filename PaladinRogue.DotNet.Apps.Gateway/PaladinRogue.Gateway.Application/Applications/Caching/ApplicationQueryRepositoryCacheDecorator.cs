using System.Threading.Tasks;
using PaladinRogue.Gateway.Domain.Applications.Persistence;
using PaladinRogue.Library.Core.Application.Caching;

namespace PaladinRogue.Gateway.Application.Applications.Caching
{
    public class ApplicationQueryRepositoryCacheDecorator : CacheDecorator<string, ApplicationCacheKey, Domain.Applications.Application>, IApplicationQueryRepository
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

        public Task<Domain.Applications.Application> GetByNameAsync(string name)
        {
            return _cacheService.GetOrAddAsync(new ApplicationCacheKey(name), () => _queryRepository.GetByNameAsync(name));
        }

        protected override ApplicationCacheKey CreateCacheKey(string key)
        {
            return new ApplicationCacheKey(key);
        }
    }
}