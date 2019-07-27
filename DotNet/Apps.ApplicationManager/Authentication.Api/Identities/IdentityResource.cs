using System;
using Authentication.ApplicationServices.Identities.Authorisation;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.Identity)]
    [SelfLink(RouteDictionary.GetIdentity, HttpVerb.Get)]
    [Link(LinkDictionary.RefreshToken, RouteDictionary.CreateRefreshToken, HttpVerb.Post, typeof(CreateRefreshTokenAuthorisationContext))]
    public class IdentityResource : IEntityResource
    {
        public Guid Id { get; set; }
    }
}