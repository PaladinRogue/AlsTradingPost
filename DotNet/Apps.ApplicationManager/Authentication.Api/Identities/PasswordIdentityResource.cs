using PaladinRogue.Authentication.Application.Identities.Authorisation;
using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.Identity)]
    [Link(LinkDictionary.ConfirmIdentity, RouteDictionary.ConfirmIdentityResourceTemplate, HttpVerb.Get, typeof(ConfirmIdentityAuthorisationContext))]
    [Link(LinkDictionary.ChangePassword, RouteDictionary.ChangePasswordResourceTemplate, HttpVerb.Get, typeof(ChangePasswordAuthorisationContext))]
    public class PasswordIdentityResource : IdentityResource
    {
    }
}