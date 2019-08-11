using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService
{
    [ResourceType(ResourceTypes.AuthenticationServicePassword)]
    [Link(LinkDictionary.Authenticate, RouteDictionary.AuthenticatePasswordResourceTemplate, HttpVerb.Get)]
    [Link(LinkDictionary.Register, RouteDictionary.RegisterPasswordResourceTemplate, HttpVerb.Get)]
    [Link(LinkDictionary.ForgotPassword, RouteDictionary.ForgotPasswordResourceTemplate, HttpVerb.Get)]
    public class PasswordAuthenticationServiceSummaryResource : AuthenticationServiceSummaryResource
    {
    }
}