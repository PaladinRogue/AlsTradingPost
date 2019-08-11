using System.Collections.Generic;
using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Gateway.Api.Aggregation
{
    [SelfLink(RouteDictionary.Aggregation, HttpVerb.Get)]
    public class ApplicationsResource : ICollectionResource<ApplicationResource>
    {
        public IList<ApplicationResource> Results { get; set; }
    }
}