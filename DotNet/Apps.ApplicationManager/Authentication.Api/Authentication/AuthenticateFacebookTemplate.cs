using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticateFacebook)]
    [SelfLink(RouteDictionary.AuthenticateFacebookResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticateFacebook)]
    public class AuthenticateFacebookTemplate : AuthenticateClientCredentialTemplate
    {
    }
}