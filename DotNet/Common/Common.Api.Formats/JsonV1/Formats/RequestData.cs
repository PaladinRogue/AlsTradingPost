using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Api.Formats.JsonV1.Formats
{
    public class RequestData
    {
        public string Type { get; set; }

        [JsonProperty(ResourceType.Attributes)]
        public JObject Attributes { get; set; }
    }
}