using Common.Authorisation.Contexts;

namespace Common.Authorisation.Policies.Deny
{
    public class AlwaysDenyAuthorisationPolicy : IAuthorisationPolicy
    {
        public bool HasAccess(IAuthorisationContext authorisationContext)
        {
            return false;
        }
    }
}
