namespace Common.Authorisation.Policies.Allow
{
    public class AlwaysAllowAuthorisationPolicy : IAuthorisationPolicy
    {
        public bool HasAccess(IAuthorisationContext authorisationContext)
        {
            return true;
        }
    }
}
