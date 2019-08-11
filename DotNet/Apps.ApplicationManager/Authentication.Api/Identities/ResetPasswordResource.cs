using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.ResetPassword)]
    [Link(LinkDictionary.Authenticate, RouteDictionary.AuthenticatePasswordResourceTemplate, HttpVerb.Get)]
    public class ResetPasswordResource : IResource
    {
    }
}