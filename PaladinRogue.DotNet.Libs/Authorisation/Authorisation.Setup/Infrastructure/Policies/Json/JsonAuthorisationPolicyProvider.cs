using Newtonsoft.Json.Linq;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Policies.Json
{
    public class JsonAuthorisationPolicyProvider : IAuthorisationPolicyProvider
    {
        public JsonAuthorisationPolicyProvider(JToken authorisationPolicy)
        {
            ResourcePolicies = authorisationPolicy.ToObject<ResourcePolicies>();
        }

        public ResourcePolicies ResourcePolicies { get; }
    }
}