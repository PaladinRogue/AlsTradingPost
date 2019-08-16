using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticateFacebook)]
    [SelfLink(RouteDictionary.AuthenticateFacebookResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticateFacebook)]
    public class AuthenticateFacebookTemplate : AuthenticateClientCredentialTemplate
    {
    }
}