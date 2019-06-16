using Newtonsoft.Json.Linq;

namespace Common.Authorisation.Policies.Json
{
    public interface IJsonAuthorisationPolicyProvider
    {
        JObject AuthorisationPolicy { get; }
    }
}
