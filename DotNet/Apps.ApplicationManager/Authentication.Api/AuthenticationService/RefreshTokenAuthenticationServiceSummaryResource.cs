using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService
{
    [ResourceType(ResourceTypes.AuthenticationServiceRefreshToken)]
    [Link(LinkDictionary.Authenticate, RouteDictionary.AuthenticateRefreshTokenResourceTemplate, HttpVerb.Get)]
    public class RefreshTokenAuthenticationServiceSummaryResource : AuthenticationServiceSummaryResource
    {
    }
}