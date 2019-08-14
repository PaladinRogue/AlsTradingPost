using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

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