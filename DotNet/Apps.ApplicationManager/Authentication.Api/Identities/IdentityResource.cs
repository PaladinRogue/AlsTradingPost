using System;
using PaladinRogue.Authentication.Application.AuthenticationServices.Authorisation;
using PaladinRogue.Authentication.Application.Identities.Authorisation;
using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.Identity)]
    [SelfLink(RouteDictionary.GetIdentity, HttpVerb.Get)]
    [Link(LinkDictionary.RefreshToken, RouteDictionary.CreateRefreshToken, HttpVerb.Post, typeof(CreateRefreshTokenAuthorisationContext))]
    [Link(LinkDictionary.AuthenticationServices, RouteDictionary.GetAuthenticationServices, HttpVerb.Get, typeof(GetAuthenticationServiceAuthorisationContext))]
    public class IdentityResource : IEntityResource
    {
        public Guid Id { get; set; }
    }
}