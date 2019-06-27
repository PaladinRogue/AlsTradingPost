using Common.Api.Concurrency;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Identities
{
    [ResourceType(ResourceTypes.ConfirmIdentity)]
    [SelfLink(RouteDictionary.ConfirmIdentityResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.ConfirmIdentity)]
    public class ConfirmIdentityTemplate : VersionedTemplate
    {
        [Required]
        public string Token { get; set; }
    }
}