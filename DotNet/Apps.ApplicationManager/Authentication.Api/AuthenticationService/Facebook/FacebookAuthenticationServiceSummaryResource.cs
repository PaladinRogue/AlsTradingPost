using System;
using PaladinRogue.Authentication.Application.AuthenticationServices.Authorisation;
using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService.Facebook
{
    [ResourceType(ResourceTypes.AuthenticationServiceFacebook)]
    [SelfLink(RouteDictionary.GetFacebookAuthenticationService, HttpVerb.Get, typeof(GetAuthenticationServiceAuthorisationContext))]
    [Link(LinkDictionary.Authenticate, RouteDictionary.AuthenticateFacebookResourceTemplate, HttpVerb.Get)]
    public class FacebookAuthenticationServiceSummaryResource : AuthenticationServiceSummaryResource, IEntityResource
    {
        public Guid Id { get; set; }

        public string AccessUrl { get; set; }

        public string Name { get; set; }
    }
}