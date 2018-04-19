using System.Collections.Generic;

namespace Common.Api.Builders.Template
{
    public class TemplateBuilderTemplate<T>
    {
        public Data<T> Data { get; set; }

        public Meta Meta { get; set; }
        
        public IList<Link> Links { get; set; }
    }
}