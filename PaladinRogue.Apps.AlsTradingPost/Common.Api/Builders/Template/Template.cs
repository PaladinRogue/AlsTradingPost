using System.Collections.Generic;

namespace Common.Api.Builders.Template
{
    public class Template
    {
        public IDictionary<string, object> Data { get; set; }

        public IDictionary<string, object> Meta { get; set; }
        
        public IDictionary<string, string> Links { get; set; }
    }
}