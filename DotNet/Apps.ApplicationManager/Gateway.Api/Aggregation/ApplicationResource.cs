using PaladinRogue.Gateway.Setup.Infrastructure.Routing;
using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Gateway.Api.Aggregation
{
    [ResourceType(ResourceTypes.Application)]
    [SelfLink(ApplicationRouteDictionary.Entrypoint, HttpVerb.Get)]
    public class ApplicationResource : IResource
    {
        public string Name { get; set; }

        public string SystemName { get; set; }
    }
}