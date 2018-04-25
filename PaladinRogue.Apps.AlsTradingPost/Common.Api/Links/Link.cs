using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

namespace Common.Api.Links
{
    public class Link
    {
        public string Name { get; set; }
        
        public string Uri { get; set; }
        
        public IDictionary<string, string> QueryParams { private get; set; }
        
        public string[] AllowVerbs { get; set; }

        public string FullUri => QueryParams != null ? QueryHelpers.AddQueryString(Uri, QueryParams) : Uri;
    }
}