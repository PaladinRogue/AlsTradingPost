using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticateGoogle)]
    [SelfLink(RouteDictionary.AuthenticateGoogleResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticateGoogle)]
    public class AuthenticateGoogleTemplate : AuthenticateClientCredentialTemplate
    {
    }
}