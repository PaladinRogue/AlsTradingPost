using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.AuthenticationService.Facebook
{
    [ResourceType(ResourceTypes.AuthenticationServiceFacebook)]
    [CreateLink(RouteDictionary.FacebookAuthenticationServiceResourceTemplate, HttpVerb.Get)]
    public class FacebookAuthenticationServiceTypeResource : AuthenticationServiceTypeResource
    {
    }
}