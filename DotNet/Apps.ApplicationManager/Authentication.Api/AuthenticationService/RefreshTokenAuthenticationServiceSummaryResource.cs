using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.AuthenticationService
{
    [ResourceType(ResourceTypes.AuthenticationServiceRefreshToken)]
    [Link(LinkDictionary.Authenticate, RouteDictionary.AuthenticateRefreshTokenResourceTemplate, HttpVerb.Get)]
    public class RefreshTokenAuthenticationServiceSummaryResource : AuthenticationServiceSummaryResource
    {
    }
}