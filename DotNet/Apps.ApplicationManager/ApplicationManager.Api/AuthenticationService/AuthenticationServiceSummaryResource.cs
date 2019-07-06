using Common.Api.Resources;

namespace ApplicationManager.Api.AuthenticationService
{
    [ResourceType(ResourceTypes.AuthenticationService)]
    public class AuthenticationServiceSummaryResource : IResource
    {
        public string Type { get; set; }
    }
}