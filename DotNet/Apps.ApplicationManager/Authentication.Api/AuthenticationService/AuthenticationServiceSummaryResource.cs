using Common.Api.Resources;

namespace Authentication.Api.AuthenticationService
{
    [ResourceType(ResourceTypes.AuthenticationService)]
    public class AuthenticationServiceSummaryResource : IResource
    {
        public string Type { get; set; }
    }
}