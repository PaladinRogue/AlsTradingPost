using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.AuthenticationService
{
    [ResourceType(ResourceTypes.AuthenticationServicePassword)]
    [Link(LinkDictionary.Authenticate, RouteDictionary.AuthenticatePasswordResourceTemplate, HttpVerb.Get)]
    [Link(LinkDictionary.Register, RouteDictionary.RegisterPasswordResourceTemplate, HttpVerb.Get)]
    public class PasswordAuthenticationServiceSummaryResource : AuthenticationServiceSummaryResource
    {
    }
}