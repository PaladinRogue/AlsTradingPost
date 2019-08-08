using System;
using System.Threading.Tasks;
using Authorisation.Application.Contexts;
using Authorisation.Application.Policies;

namespace Authorisation.Application.Manager
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
