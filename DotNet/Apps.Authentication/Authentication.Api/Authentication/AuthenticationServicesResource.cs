using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Authentication
{
    [SelfLink(RouteDictionary.AuthenticationServices, HttpVerb.Get)]
    [Link(LinkDictionary.AuthenticationFacebookTemplate, RouteDictionary.AuthenticationFacebookTemplate, HttpVerb.Get)]
    [Link(LinkDictionary.AuthenticationRefreshTokenTemplate, RouteDictionary.AuthenticationRefreshTokenTemplate, HttpVerb.Get)]
    public class AuthenticationServicesResource : IResource
    {
    }
}