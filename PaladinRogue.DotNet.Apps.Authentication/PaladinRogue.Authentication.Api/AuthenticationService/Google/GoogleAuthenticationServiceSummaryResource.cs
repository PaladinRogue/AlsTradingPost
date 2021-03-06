using System;
using PaladinRogue.Authentication.Application.AuthenticationServices.Authorisation;
using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService.Google
{
    [ResourceType(ResourceTypes.AuthenticationServiceGoogle)]
    [SelfLink(RouteDictionary.GetGoogleAuthenticationService, HttpVerb.Get, typeof(GetAuthenticationServiceAuthorisationContext))]
    [Link(LinkDictionary.Authenticate, RouteDictionary.AuthenticateGoogleResourceTemplate, HttpVerb.Get)]
    public class GoogleAuthenticationServiceSummaryResource : AuthenticationServiceSummaryResource, IEntityResource
    {
        public Guid Id { get; set; }

        public string AccessUrl { get; set; }

        public string Name { get; set; }
    }
}