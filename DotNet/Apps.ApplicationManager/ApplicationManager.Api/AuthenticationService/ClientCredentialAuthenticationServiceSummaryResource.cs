using System;
using Common.Api.Resources;

namespace ApplicationManager.Api.AuthenticationService
{
    [ResourceType(ResourceTypes.AuthenticationService)]
    public class ClientCredentialAuthenticationServiceSummaryResource : AuthenticationServiceSummaryResource, IEntityResource
    {
        public Guid Id { get; set; }

        public string AccessUrl { get; set; }
    }
}