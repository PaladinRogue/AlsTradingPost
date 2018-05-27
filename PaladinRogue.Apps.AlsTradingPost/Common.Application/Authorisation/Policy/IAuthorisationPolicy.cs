namespace Common.Application.Authorisation.Policy
{
    public interface IAuthorisationPolicy
    {
        bool HasAccess(string resource, string action, IAuthorisationContext authorisationContext);
    }
}
