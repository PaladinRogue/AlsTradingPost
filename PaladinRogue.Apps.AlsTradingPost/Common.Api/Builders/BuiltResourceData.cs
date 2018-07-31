using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Api.Builders
{
    public class BuiltResourceData
    {
        public Guid Id { get; set; }

        [JsonProperty(ResourceType.Type)]
        public string Type { get; set; }

        [JsonProperty(ResourceType.Attributes)]
        public IDictionary<string, object> Attributes { get; set; }
    }
}
