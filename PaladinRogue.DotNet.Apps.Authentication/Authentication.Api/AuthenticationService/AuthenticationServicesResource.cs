using System.Collections.Generic;
using PaladinRogue.Authentication.Application.AuthenticationServices.Authorisation;
using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService
{
    [SelfLink(RouteDictionary.GetAuthenticationServices, HttpVerb.Get)]
    [CreateLink(RouteDictionary.GetAuthenticationServiceResourceTemplateTypes, HttpVerb.Get, typeof(CreateAuthenticationServiceAuthorisationContext))]
    public class AuthenticationServicesResource : ICollectionResource<AuthenticationServiceSummaryResource>
    {
        public IList<AuthenticationServiceSummaryResource> Results { get; set; }
    }
}