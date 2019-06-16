namespace Common.Authorisation.Policies
{
    public interface IAuthorisationPolicy
    {
        bool HasAccess(IAuthorisationContext authorisationContext);
    }
}
