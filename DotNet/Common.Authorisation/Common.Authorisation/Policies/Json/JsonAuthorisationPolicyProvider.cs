using Newtonsoft.Json.Linq;

namespace Common.Authorisation.Policies.Json
{
    public class JsonAuthorisationPolicyProvider : IJsonAuthorisationPolicyProvider
    {
        public JsonAuthorisationPolicyProvider(JObject authorisationPolicy)
        {
            AuthorisationPolicy = authorisationPolicy;
        }

        public JObject AuthorisationPolicy { get; }
    }
}
