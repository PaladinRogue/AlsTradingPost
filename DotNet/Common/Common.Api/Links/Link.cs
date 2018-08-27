using System.Collections.Generic;
using Common.Setup.Infrastructure.Constants;

namespace Common.Api.Links
{
    public class Link : ILink
    {
        public string Name { get; set; }
        
        public string Uri { get; set; }
        
        public IDictionary<string, object> QueryParams { get; set; }
        
        public HttpVerb AllowVerbs { get; set; }
    }
}