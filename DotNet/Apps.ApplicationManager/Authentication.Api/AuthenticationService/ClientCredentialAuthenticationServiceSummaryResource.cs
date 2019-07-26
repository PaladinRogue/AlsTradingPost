using System;
using Authentication.ApplicationServices.AuthenticationServices.Authorisation;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.AuthenticationService
{
    [ResourceType(ResourceTypes.AuthenticationServiceClientCredential)]
    [SelfLink(RouteDictionary.GetAuthenticationService, HttpVerb.Get, typeof(GetAuthenticationServiceAuthorisationContext))]
    public class ClientCredentialAuthenticationServiceSummaryResource : AuthenticationServiceSummaryResource, IEntityResource
    {
        public Guid Id { get; set; }

        public string AccessUrl { get; set; }

        public string Name { get; set; }
    }
}