using System.Collections.Generic;
using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Gateway.Api.Aggregation
{
    [SelfLink(RouteDictionary.Aggregation, HttpVerb.Get)]
    public class ApplicationsResource : ICollectionResource<ApplicationResource>
    {
        public IList<ApplicationResource> Results { get; set; }
    }
}