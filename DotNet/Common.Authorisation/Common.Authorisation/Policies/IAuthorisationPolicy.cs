using Common.Authorisation.Contexts;

namespace Common.Authorisation.Policies
{
    public interface IAuthorisationPolicy
    {
        bool HasAccess(IAuthorisationContext authorisationContext);
    }
}
