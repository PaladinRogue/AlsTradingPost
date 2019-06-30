using ApplicationManager.ApplicationServices;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Status
{
    [SelfLink(RouteDictionary.Status, HttpVerb.Get)]
    public class StatusResource : IResource
    {
    }
}
