using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Common.Api.ResourceFormatter.Attributes.Meta;
using Common.Api.ResourceFormatter.Attributes.Resource;

namespace Common.Api.ResourceFormatter.Resources
{
    public class Resource
    {
        public Resource(object resource)
        {
            Data = ResourceFormatterHelper.FormatResourceData(resource);
            Meta = ResourceFormatterHelper.FormatResourceMeta(resource);
        }

        public IDictionary<string, object> Data { get; set; }

        public IDictionary<string, object> Meta { get; set; }
    }
}