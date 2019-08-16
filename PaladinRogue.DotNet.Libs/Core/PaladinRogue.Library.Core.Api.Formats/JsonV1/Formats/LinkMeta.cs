using System.Collections.Generic;
using Newtonsoft.Json;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Formats
{
    public class LinkMeta
    {
        [JsonProperty(LinkPartType.AllowVerbs)]
        public IEnumerable<string> AllowVerbs { get; set; }
    }
}
