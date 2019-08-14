using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.ForgotPassword)]
    [Link(LinkDictionary.ResetPassword, RouteDictionary.ResetPasswordResourceTemplate, HttpVerb.Get)]
    public class ForgotPasswordResource : IResource
    {
    }
}