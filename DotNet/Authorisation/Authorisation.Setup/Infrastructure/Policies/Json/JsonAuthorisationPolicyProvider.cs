using Newtonsoft.Json.Linq;

namespace Authorisation.Application.Policies.Json
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