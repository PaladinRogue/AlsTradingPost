using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Api.Builders
{
    public class BuiltResource : IBuiltResource
    {
        [JsonProperty(ResourceType.Data)]
        public object Data { get; set; }
        
        [JsonProperty(ResourceType.Meta)]
        public IDictionary<string, Dictionary<string, object>> Meta { get; set; }
        
        [JsonProperty(ResourceType.Links)]
        public IDictionary<string, IDictionary<string, object>> Links { get; set; }
    }
}