using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

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