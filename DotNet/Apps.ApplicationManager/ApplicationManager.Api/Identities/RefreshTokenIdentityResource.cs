using ApplicationManager.ApplicationServices;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Identities
{
    [ResourceType(ResourceTypes.RefreshToken)]
    [SelfLink(RouteDictionary.CreateRefreshToken, HttpVerb.Post)]
    public class RefreshTokenIdentityResource : IResource
    {
        public string Token { get; set; }
    }
}