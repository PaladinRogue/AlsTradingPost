using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticateGoogle)]
    [SelfLink(RouteDictionary.AuthenticateGoogleResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticateGoogle)]
    public class AuthenticateGoogleTemplate : AuthenticateClientCredentialTemplate
    {
    }
}