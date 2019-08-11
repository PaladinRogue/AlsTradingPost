using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PaladinRogue.Libray.Core.Api.Formats.JsonV1.Formats
{
    public class Data
    {
        public string Type { get; set; }

        public Guid? Id { get; set; }

        [JsonProperty(ResourceType.Attributes)]
        public IDictionary<string, object> Attributes { get; set; }

        [JsonProperty(ResourceType.Meta)]
        public IDictionary<string, object> Meta { get; set; }

        [JsonProperty(ResourceType.Links)]
        public IDictionary<string, Link> Links { get; set; }
    }
}
