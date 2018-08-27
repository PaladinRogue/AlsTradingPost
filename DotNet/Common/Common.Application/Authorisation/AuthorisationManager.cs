using System;
using Common.Application.Authorisation.Policy;

namespace Common.Application.Authorisation
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
