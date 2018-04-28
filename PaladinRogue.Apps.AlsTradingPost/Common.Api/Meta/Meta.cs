using System.Collections.Generic;

namespace Common.Api.Meta
{
    public class Meta
    {
        public string TemplateTypeName { get; set; }
        
        public IList<PropertyMeta> Properties { get; set; }
    }
}