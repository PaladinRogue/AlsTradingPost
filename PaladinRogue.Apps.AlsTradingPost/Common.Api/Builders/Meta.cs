using System.Collections.Generic;

namespace Common.Api.Builders
{
    public class Meta
    {
        public string TemplateTypeName { get; set; }
        
        public IList<PropertyMeta> Properties { get; set; }
    }
}