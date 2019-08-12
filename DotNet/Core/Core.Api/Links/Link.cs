using System.Collections.Generic;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Library.Core.Api.Links
{
    public class Link : ILink
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public IDictionary<string, object> QueryParams { get; set; }

        public HttpVerb AllowVerbs { get; set; }
    }
}