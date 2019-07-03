using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticateClientCredential)]
    [SelfLink(RouteDictionary.AuthenticateClientCredentialResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticateClientCredential)]
    public class ClientCredentialTemplate : ITemplate
    {
        [Required]
        public string RedirectUri { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string State { get; set; }
    }
}