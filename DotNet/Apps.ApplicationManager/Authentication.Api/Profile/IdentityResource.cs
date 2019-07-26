using System;
using Authentication.ApplicationServices.Identities.Authorisation;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Profile
{
    [ResourceType(ResourceTypes.Profile)]
    [SelfLink(RouteDictionary.Profile, HttpVerb.Get)]
    [Link(LinkDictionary.ChangePassword, RouteDictionary.ChangePasswordResourceTemplate, HttpVerb.Get, typeof(ChangePasswordAuthorisationContext))]
    [Link(LinkDictionary.ConfirmIdentity, RouteDictionary.ConfirmIdentityResourceTemplate, HttpVerb.Get, typeof(ConfirmIdentityAuthorisationContext))]
    [Link(LinkDictionary.RefreshToken, RouteDictionary.CreateRefreshToken, HttpVerb.Post, typeof(CreateRefreshTokenAuthorisationContext))]
    public class IdentityResource : IEntityResource
    {
        public Guid Id { get; set; }
    }
}