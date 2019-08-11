using System.Collections.Generic;
using PaladinRogue.Authentication.Application.AuthenticationServices.Authorisation;
using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.AuthenticationService
{
    [SelfLink(RouteDictionary.GetAuthenticationServices, HttpVerb.Get)]
    [CreateLink(RouteDictionary.GetAuthenticationServiceResourceTemplateTypes, HttpVerb.Get, typeof(CreateAuthenticationServiceAuthorisationContext))]
    public class AuthenticationServicesResource : ICollectionResource<AuthenticationServiceSummaryResource>
    {
        public IList<AuthenticationServiceSummaryResource> Results { get; set; }
    }
}