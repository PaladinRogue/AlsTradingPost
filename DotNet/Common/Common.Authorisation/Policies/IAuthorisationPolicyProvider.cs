namespace Common.Authorisation.Policies
{
    public interface IAuthorisationPolicyProvider
    {
        ResourcePolicies ResourcePolicies { get; }
    }
}
