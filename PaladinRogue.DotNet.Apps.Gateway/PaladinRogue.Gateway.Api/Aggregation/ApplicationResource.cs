using PaladinRogue.Gateway.Setup.Infrastructure.Routing;
using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

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