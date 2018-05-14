using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Status
{
    [SelfLink(RouteDictionary.Status, HttpVerbs.Get)]
    public class StatusResource : IResource
    {
    }
}
