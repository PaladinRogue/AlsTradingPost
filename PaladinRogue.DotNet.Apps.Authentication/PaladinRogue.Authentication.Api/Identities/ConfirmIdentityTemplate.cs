using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Api.Validation.Attributes;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Identities
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