using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticateFacebook)]
    [SelfLink(RouteDictionary.AuthenticateFacebookResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticateFacebook)]
    public class AuthenticateFacebookTemplate : AuthenticateClientCredentialTemplate
    {
    }
}