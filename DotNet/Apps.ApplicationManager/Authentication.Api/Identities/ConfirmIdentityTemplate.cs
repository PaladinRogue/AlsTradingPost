using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.ConfirmIdentity)]
    [SelfLink(RouteDictionary.ConfirmIdentityResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.ConfirmIdentity)]
    [Link(LinkDictionary.ResendConfirmIdentity, RouteDictionary.ResendConfirmIdentity, HttpVerb.Post)]
    public class ConfirmIdentityTemplate : ITemplate
    {
        [Required]
        public string Token { get; set; }
    }
}