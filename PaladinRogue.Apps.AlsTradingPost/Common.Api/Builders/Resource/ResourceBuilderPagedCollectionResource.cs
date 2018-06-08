using System.Collections.Generic;

namespace Common.Api.Builders.Resource
{
    public class ResourceBuilderPagedCollectionResource
    {
        public int TotalResults { get; set; }
        public IEnumerable<IBuiltResource> Results { get; set; }
    }
}