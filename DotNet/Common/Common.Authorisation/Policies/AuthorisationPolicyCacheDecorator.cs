using System.Threading.Tasks;
using Common.ApplicationServices.Caching;
using Common.Authorisation.Contexts;

namespace Common.Authorisation.Policies
{
    public class AuthorisationPolicyCacheDecorator : IAuthorisationPolicy
    {
        private readonly IAuthorisationPolicy _authorisationPolicy;

        private readonly ICacheService _cacheService;

        public AuthorisationPolicyCacheDecorator(
            IAuthorisationPolicy authorisationPolicy,
            ICacheService cacheService)
        {
            _authorisationPolicy = authorisationPolicy;
            _cacheService = cacheService;
        }

        public Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext)
        {
            return _cacheService.GetOrAddAsync(new AuthorisationContextCacheKey(authorisationContext), () => _authorisationPolicy.HasAccessAsync(authorisationContext));
        }
    }
}