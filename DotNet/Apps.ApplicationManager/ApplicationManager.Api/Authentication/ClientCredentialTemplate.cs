using System;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticateClientCredential)]
    [SelfLink(RouteDictionary.AuthenticateClientCredentialResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticateClientCredential)]
    public class ClientCredentialTemplate : ITemplate
    {
        [Ignore]
        public Guid Id { get; set; }

        [Required]
        public string RedirectUri { get; set; }

        [Required]
        public string Token { get; set; }
    }
}