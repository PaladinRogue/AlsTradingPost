﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Formats
{
    public class FormattedArrayResponse
    {
        [JsonProperty(ResourceType.Data)]
        public IEnumerable<Data> Data { get; set; }

        [JsonProperty(ResourceType.Meta)]
        public IDictionary<string, object> Meta { get; set; }

        [JsonProperty(ResourceType.Links)]
        public IDictionary<string, Link> Links { get; set; }
    }
}
