namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies
{
    public interface IAuthorisationPolicyProvider
    {
        ResourcePolicies ResourcePolicies { get; }
    }
}
