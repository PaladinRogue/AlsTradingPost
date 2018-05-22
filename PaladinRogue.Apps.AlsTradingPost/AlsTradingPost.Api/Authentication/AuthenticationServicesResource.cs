using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Authentication
{
    [SelfLink(RouteDictionary.AuthenticationServices, HttpVerbs.Get)]
    [Link(LinkDictionary.AuthenticationLoginTemplate, RouteDictionary.AuthenticationLoginTemplate, HttpVerbs.Get)]
    [Link(LinkDictionary.AuthenticationRefreshTokenTemplate, RouteDictionary.AuthenticationRefreshTokenTemplate, HttpVerbs.Get)]
    public class AuthenticationServicesResource : IResource
    {
    }
}