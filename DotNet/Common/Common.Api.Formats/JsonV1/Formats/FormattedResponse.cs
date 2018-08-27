using System.Collections.Generic;
using Common.Api.Builders;
using Newtonsoft.Json;

namespace Common.Api.Formats.JsonV1.Formats
{
    public class FormattedResponse
    {
        [JsonProperty(ResourceType.Data)]
        public Data Data { get; set; }

        [JsonProperty(ResourceType.Links)]
        public IDictionary<string, Link> Links { get; set; }
    }
}
