using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Authentication
{
    [SelfLink(RouteDictionary.AuthenticationServices, HttpVerbs.Get)]
    [Link(LinkDictionary.AuthenticationFacebookTemplate, RouteDictionary.AuthenticationFacebookTemplate, HttpVerbs.Get)]
    [Link(LinkDictionary.AuthenticationRefreshTokenTemplate, RouteDictionary.AuthenticationRefreshTokenTemplate, HttpVerbs.Get)]
    public class AuthenticationServicesResource : IResource
    {
    }
}