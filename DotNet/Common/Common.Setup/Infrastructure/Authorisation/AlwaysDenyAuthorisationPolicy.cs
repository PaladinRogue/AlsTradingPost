using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;

namespace Common.Setup.Infrastructure.Authorisation
{
    public class AlwaysDenyAuthorisationPolicy : IAuthorisationPolicy
    {
        public bool HasAccess(IAuthorisationContext authorisationContext)
        {
            return false;
        }
    }
}
