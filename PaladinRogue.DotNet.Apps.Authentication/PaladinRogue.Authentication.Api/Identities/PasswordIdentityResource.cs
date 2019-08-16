using PaladinRogue.Authentication.Application.Identities.Authorisation;
using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.Identity)]
    [Link(LinkDictionary.ConfirmIdentity, RouteDictionary.ConfirmIdentityResourceTemplate, HttpVerb.Get, typeof(ConfirmIdentityAuthorisationContext))]
    [Link(LinkDictionary.ChangePassword, RouteDictionary.ChangePasswordResourceTemplate, HttpVerb.Get, typeof(ChangePasswordAuthorisationContext))]
    public class PasswordIdentityResource : IdentityResource
    {
    }
}