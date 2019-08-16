using Newtonsoft.Json;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Formats
{
    public class Link
    {
        [JsonProperty(LinkPartType.Href)]
        public string Href { get; set; }

        [JsonProperty(LinkPartType.Meta)]
        public LinkMeta Meta { get; set; }
    }
}
