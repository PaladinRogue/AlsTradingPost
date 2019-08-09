using System;
using Authentication.Application.AuthenticationServices.Authorisation;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.AuthenticationService.Facebook
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