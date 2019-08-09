using Authentication.Application.Identities.Authorisation;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.Identity)]
    [Link(LinkDictionary.ConfirmIdentity, RouteDictionary.ConfirmIdentityResourceTemplate, HttpVerb.Get, typeof(ConfirmIdentityAuthorisationContext))]
    [Link(LinkDictionary.ChangePassword, RouteDictionary.ChangePasswordResourceTemplate, HttpVerb.Get, typeof(ChangePasswordAuthorisationContext))]
    public class PasswordIdentityResource : IdentityResource
    {
    }
}