using System;
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

        public bool HasAccess(IAuthorisationContext authorisationContext)
        {
            return _authorisationPolicy.HasAccess(authorisationContext);
        }

        public void DemandAccess(IAuthorisationContext authorisationContext)
        {
            if (!HasAccess(authorisationContext))
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
