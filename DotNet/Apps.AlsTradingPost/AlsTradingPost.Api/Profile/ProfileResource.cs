using AlsTradingPost.Setup.Infrastructure.Links;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Profile
{
    [SelfLink(RouteDictionary.Profile, HttpVerb.Get)]
    [DynamicLinks(typeof(AuthenticationDynmicLinksProvider))]
    public class ProfileResource : IResource
    {
    }
}
