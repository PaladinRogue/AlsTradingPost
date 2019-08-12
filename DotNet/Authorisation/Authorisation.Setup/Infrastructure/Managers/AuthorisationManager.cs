using System;
using System.Threading.Tasks;
using PaladinRogue.Library.Authorisation.Common.Contexts;
using PaladinRogue.Library.Authorisation.Common.Managers;
using PaladinRogue.Library.Authorisation.Common.Policies;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Managers
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
