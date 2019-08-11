namespace PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Policies
{
    public interface IAuthorisationPolicyProvider
    {
        ResourcePolicies ResourcePolicies { get; }
    }
}
