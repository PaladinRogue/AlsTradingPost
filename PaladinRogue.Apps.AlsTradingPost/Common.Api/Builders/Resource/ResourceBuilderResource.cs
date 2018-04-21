using System.Collections.Generic;

namespace Common.Api.Builders.Resource
{
    public class ResourceBuilderResource<T>
    {
        public Data<T> Data { get; set; }

        public Meta Meta { get; set; }
        
        public IList<Link> Links { get; set; }
    }
}