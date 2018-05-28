using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Status
{
    [SelfLink(RouteDictionary.Status, HttpVerbs.Get)]
    public class StatusResource : IResource
    {
    }
}
