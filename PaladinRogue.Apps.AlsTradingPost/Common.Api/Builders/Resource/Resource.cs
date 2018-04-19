using System.Collections.Generic;

namespace Common.Api.Builders.Resource
{
    public class Resource
    {
        public IDictionary<string, object> Data { get; set; }

        public IDictionary<string, object> Meta { get; set; }
        
        public IDictionary<string, string> Links { get; set; }
    }
}