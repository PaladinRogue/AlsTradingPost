using System;
using Authentication.ApplicationServices.AuthenticationServices.Authorisation;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.AuthenticationService.Google
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