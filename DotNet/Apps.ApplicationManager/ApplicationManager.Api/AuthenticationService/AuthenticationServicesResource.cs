using System.Collections.Generic;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.AuthenticationService
{
    [SelfLink(RouteDictionary.GetAuthenticationServices, HttpVerb.Get)]
    public class AuthenticationServicesResource : ICollectionResource<AuthenticationServiceSummaryResource>
    {
        public IList<AuthenticationServiceSummaryResource> Results { get; set; }
    }
}