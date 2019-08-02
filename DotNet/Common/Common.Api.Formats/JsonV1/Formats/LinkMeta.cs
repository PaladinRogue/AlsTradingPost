using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Api.Formats.JsonV1.Formats
{
    public class LinkMeta
    {
        [JsonProperty(LinkPartType.AllowVerbs)]
        public IEnumerable<string> AllowVerbs { get; set; }
    }
}
