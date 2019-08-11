using System.Threading.Tasks;
using PaladinRogue.Libray.Authorisation.Common.Contexts;
using PaladinRogue.Libray.Authorisation.Common.Policies;
using PaladinRogue.Libray.Core.Application.Caching;

namespace PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Policies
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