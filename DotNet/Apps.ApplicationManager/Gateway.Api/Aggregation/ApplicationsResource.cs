using System.Collections.Generic;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Gateway.Api.Aggregation
{
    [SelfLink(RouteDictionary.Aggregation, HttpVerb.Get)]
    public class ApplicationsResource : ICollectionResource<ApplicationResource>
    {
        public IList<ApplicationResource> Results { get; set; }
    }
}