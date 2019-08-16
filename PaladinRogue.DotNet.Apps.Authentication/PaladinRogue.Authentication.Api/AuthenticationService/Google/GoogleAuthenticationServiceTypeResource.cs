using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService.Google
{
    [ResourceType(ResourceTypes.AuthenticationServiceGoogle)]
    [CreateLink(RouteDictionary.GoogleAuthenticationServiceResourceTemplate, HttpVerb.Get)]
    public class GoogleAuthenticationServiceTypeResource : AuthenticationServiceTypeResource
    {
    }
}