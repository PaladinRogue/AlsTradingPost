using System.Collections.Generic;
using Newtonsoft.Json;

namespace PaladinRogue.Libray.Core.Api.Formats.JsonV1.Formats
{
    public class FormattedResponse
    {
        [JsonProperty(ResourceType.Data)]
        public Data Data { get; set; }

        [JsonProperty(ResourceType.Links)]
        public IDictionary<string, Link> Links { get; set; }
    }
}
