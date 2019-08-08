namespace Authorisation.Application.Policies
{
    public interface IAuthorisationPolicyProvider
    {
        ResourcePolicies ResourcePolicies { get; }
    }
}
