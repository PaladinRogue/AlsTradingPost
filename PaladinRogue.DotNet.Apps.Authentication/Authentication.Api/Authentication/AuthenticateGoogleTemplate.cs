using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticateGoogle)]
    [SelfLink(RouteDictionary.AuthenticateGoogleResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticateGoogle)]
    public class AuthenticateGoogleTemplate : AuthenticateClientCredentialTemplate
    {
    }
}