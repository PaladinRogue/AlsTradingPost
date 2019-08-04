using System;
using System.Threading.Tasks;
using Common.Authorisation.Contexts;
using Common.Authorisation.Policies;

namespace Common.Authorisation.Manager
{
    public class AuthorisationManager : IAuthorisationManager
    {
        private readonly IAuthorisationPolicy _authorisationPolicy;

        public AuthorisationManager(IAuthorisationPolicy authorisationPolicy)
        {
            _authorisationPolicy = authorisationPolicy;
        }

        public Task<bool> HasAccessAsync(IAuthorisationContext authorisationContext)
        {
            return _authorisationPolicy.HasAccessAsync(authorisationContext);
        }

        public async Task DemandAccessAsync(IAuthorisationContext authorisationContext)
        {
            if (!await HasAccessAsync(authorisationContext))
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
