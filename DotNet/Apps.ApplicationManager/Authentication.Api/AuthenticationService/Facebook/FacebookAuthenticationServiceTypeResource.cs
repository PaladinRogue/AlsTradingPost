using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService.Facebook
{
    [ResourceType(ResourceTypes.AuthenticationServiceFacebook)]
    [CreateLink(RouteDictionary.FacebookAuthenticationServiceResourceTemplate, HttpVerb.Get)]
    public class FacebookAuthenticationServiceTypeResource : AuthenticationServiceTypeResource
    {
    }
}