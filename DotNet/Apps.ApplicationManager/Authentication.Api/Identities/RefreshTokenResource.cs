using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.RefreshToken)]
    [SelfLink(RouteDictionary.CreateRefreshToken, HttpVerb.Post)]
    [Link(LinkDictionary.Authenticate, RouteDictionary.AuthenticateRefreshTokenResourceTemplate, HttpVerb.Get)]
    public class RefreshTokenResource : IResource
    {
        public string Token { get; set; }
    }
}