using AlsTradingPost.Setup.Infrastructure.Links;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Authentication
{
    [SelfLink(RouteDictionary.AuthenticationServices, HttpVerb.Get)]
    [Link(LinkDictionary.AuthenticationLoginTemplate, RouteDictionary.AuthenticationLoginTemplate, HttpVerb.Get)]
    [Link(LinkDictionary.AuthenticationRefreshTokenTemplate, RouteDictionary.AuthenticationRefreshTokenTemplate, HttpVerb.Get)]
    public class AuthenticationServicesResource : IResource
    {
    }
}