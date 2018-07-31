using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Api.Builders
{
    public class BuiltCollectionResource : IBuiltResource
    {
        [JsonProperty(ResourceType.Data)]
        public BuiltResourceData Data { get; set; }
        
        [JsonProperty(ResourceType.Meta)]
        public IDictionary<string, Dictionary<string, object>> Meta { get; set; }
        
        [JsonProperty(ResourceType.RelatedLinks)]
        public IDictionary<string, IDictionary<string, object>> Links { get; set; }
    }
}