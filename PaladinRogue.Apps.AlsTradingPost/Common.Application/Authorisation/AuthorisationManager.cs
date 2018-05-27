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

        public bool HasAccess(IAuthorisationRule authorisationRule)
        {
            return _authorisationPolicy.HasAccess(authorisationRule.Resource, authorisationRule.Action);
        }

        public void DemandAccess(IAuthorisationRule authorisationRule)
        {
            if (!HasAccess(authorisationRule))
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
