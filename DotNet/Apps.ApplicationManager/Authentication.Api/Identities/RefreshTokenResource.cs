using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.RefreshToken)]
    [SelfLink(RouteDictionary.CreateRefreshToken, HttpVerb.Post)]
    [Link(LinkDictionary.Authenticate, RouteDictionary.AuthenticateRefreshTokenResourceTemplate, HttpVerb.Get)]
    public class RefreshTokenResource : IResource
    {
        public string Token { get; set; }
    }
}