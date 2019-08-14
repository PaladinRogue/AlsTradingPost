using System;
using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Formats
{
    public class Error
    {
        public Guid? Id { get; set; }

        public Dictionary<string, object> Links { get; set; }

        public int? Status { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public Dictionary<string, object> Meta { get; set; }
    }
}
