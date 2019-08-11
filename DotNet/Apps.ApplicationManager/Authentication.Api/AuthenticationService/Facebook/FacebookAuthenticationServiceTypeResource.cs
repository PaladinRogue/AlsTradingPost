using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService.Facebook
{
    [ResourceType(ResourceTypes.AuthenticationServiceFacebook)]
    [CreateLink(RouteDictionary.FacebookAuthenticationServiceResourceTemplate, HttpVerb.Get)]
    public class FacebookAuthenticationServiceTypeResource : AuthenticationServiceTypeResource
    {
    }
}