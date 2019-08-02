using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.AuthenticationService.Google
{
    [ResourceType(ResourceTypes.AuthenticationServiceGoogle)]
    [CreateLink(RouteDictionary.GoogleAuthenticationServiceResourceTemplate, HttpVerb.Get)]
    public class GoogleAuthenticationServiceTypeResource : AuthenticationServiceTypeResource
    {
    }
}