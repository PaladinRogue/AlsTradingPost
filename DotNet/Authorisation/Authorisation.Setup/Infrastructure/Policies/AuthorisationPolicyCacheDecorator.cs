using System.Threading.Tasks;
using PaladinRogue.Library.Authorisation.Common.Contexts;
using PaladinRogue.Library.Authorisation.Common.Policies;
using PaladinRogue.Library.Core.Application.Caching;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies
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