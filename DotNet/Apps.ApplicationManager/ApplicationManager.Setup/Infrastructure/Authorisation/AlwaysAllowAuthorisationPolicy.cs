using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;

namespace ApplicationManager.Setup.Infrastructure.Authorisation
{
    public class AlwaysAllowAuthorisationPolicy : IAuthorisationPolicy
    {
        public bool HasAccess(IAuthorisationContext authorisationContext)
        {
            return true;
        }
    }
}
