using System.Collections.Generic;
using Authentication.Application.AuthenticationServices.Authorisation;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.AuthenticationService
{
    [SelfLink(RouteDictionary.GetAuthenticationServices, HttpVerb.Get)]
    [CreateLink(RouteDictionary.GetAuthenticationServiceResourceTemplateTypes, HttpVerb.Get, typeof(CreateAuthenticationServiceAuthorisationContext))]
    public class AuthenticationServicesResource : ICollectionResource<AuthenticationServiceSummaryResource>
    {
        public IList<AuthenticationServiceSummaryResource> Results { get; set; }
    }
}