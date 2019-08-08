using System.Threading.Tasks;
using Authorisation.Application.Contexts;
using Common.ApplicationServices.Caching;

namespace Authorisation.Application.Policies
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