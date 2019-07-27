using System.Collections.Generic;
using Authentication.ApplicationServices.AuthenticationServices.Authorisation;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.AuthenticationService
{
    [SelfLink(RouteDictionary.GetAuthenticationServices, HttpVerb.Get)]
    [CreateLink(RouteDictionary.CreateAuthenticationService, HttpVerb.Post, typeof(CreateAuthenticationServiceAuthorisationContext))]
    public class AuthenticationServicesResource : ICollectionResource<AuthenticationServiceSummaryResource>
    {
        public IList<AuthenticationServiceSummaryResource> Results { get; set; }
    }
}