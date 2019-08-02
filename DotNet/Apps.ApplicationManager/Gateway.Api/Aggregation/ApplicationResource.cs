using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;
using Gateway.Setup.Infrastructure.Routing;

namespace Gateway.Api.Aggregation
{
    [ResourceType(ResourceTypes.Application)]
    [SelfLink(ApplicationRouteDictionary.Entrypoint, HttpVerb.Get)]
    public class ApplicationResource : IResource
    {
        public string Name { get; set; }

        public string SystemName { get; set; }
    }
}