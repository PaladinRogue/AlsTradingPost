namespace Common.Application.Authorisation.Policy
{
    public interface IAuthorisationPolicy
    {
        bool HasAccess(IAuthorisationContext authorisationContext);
    }
}
