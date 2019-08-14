using Newtonsoft.Json;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Formats
{
    public class Request
    {
        [JsonProperty(ResourceType.Data)]
        public RequestData Data { get; set; }
    }
}