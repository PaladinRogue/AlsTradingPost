using Newtonsoft.Json.Linq;

namespace Common.Application.Authorisation.Policy
{
    public interface IJsonAuthorisationPolicyProvider
    {
        JObject AuthorisationPolicy { get; }
    }
}
