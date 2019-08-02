using Newtonsoft.Json;

namespace Common.Api.Formats.JsonV1.Formats
{
    public class Request
    {
        [JsonProperty(ResourceType.Data)]
        public RequestData Data { get; set; }
    }
}