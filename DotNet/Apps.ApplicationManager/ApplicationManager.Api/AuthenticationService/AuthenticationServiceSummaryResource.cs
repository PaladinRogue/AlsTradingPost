using Common.Api.Resources;

namespace ApplicationManager.Api.AuthenticationService
{
    [ResourceType(ResourceTypes.AuthenticationService)]
    public class AuthenticationServiceSummaryResource : ITemplate
    {
        public string Type { get; set; }

        public string AccessUrl { get; set; }
    }
}