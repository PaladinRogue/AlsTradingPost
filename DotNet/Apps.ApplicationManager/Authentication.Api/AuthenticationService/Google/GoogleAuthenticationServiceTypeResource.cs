using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService.Google
{
    [ResourceType(ResourceTypes.AuthenticationServiceGoogle)]
    [CreateLink(RouteDictionary.GoogleAuthenticationServiceResourceTemplate, HttpVerb.Get)]
    public class GoogleAuthenticationServiceTypeResource : AuthenticationServiceTypeResource
    {
    }
}