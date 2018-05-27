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

        public bool HasAccess(IAuthorisationRule authorisationRule, IAuthorisationContext authorisationContext)
        {
            return _authorisationPolicy.HasAccess(authorisationRule.Resource, authorisationRule.Action, authorisationContext);
        }

        public void DemandAccess(IAuthorisationRule authorisationRule, IAuthorisationContext authorisationContext)
        {
            if (!HasAccess(authorisationRule, authorisationContext))
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
