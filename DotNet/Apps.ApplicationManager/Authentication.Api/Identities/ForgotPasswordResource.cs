using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.ForgotPassword)]
    [Link(LinkDictionary.ResetPassword, RouteDictionary.ResetPasswordResourceTemplate, HttpVerb.Get)]
    public class ForgotPasswordResource : IResource
    {
    }
}