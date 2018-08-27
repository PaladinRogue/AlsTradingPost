using Newtonsoft.Json.Linq;

namespace Common.Application.Authorisation.Policy
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
