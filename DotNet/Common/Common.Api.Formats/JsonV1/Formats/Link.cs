using System.Collections.Generic;
using Common.Api.Links;
using Newtonsoft.Json;

namespace Common.Api.Formats.JsonV1.Formats
{
    public class Link
    {
        [JsonProperty(LinkPartType.Href)]
        public string Href { get; set; }

        [JsonProperty(LinkPartType.Meta)]
        public LinkMeta Meta { get; set; }
    }
}
