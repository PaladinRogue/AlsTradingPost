using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Api.Builders.Resource
{
    public class ResourceBuilder<T>
    {
        private readonly ResourceBuilderResource<T> _resource;

        private ResourceBuilder(T resource)
        {
            _resource = new ResourceBuilderResource<T>
            {
                Data = BuilderHelper.FormatResourceData(resource)
            };
        }

        public static ResourceBuilder<T> Create(T resource)
        {
            return new ResourceBuilder<T>(resource);
        }

        public ResourceBuilder<T> WithMeta<TTemplate>(TTemplate template)
        {
            _resource.Meta = BuilderHelper.FormatMeta(template);

            return this;
        }

        public Resource Build()
        {
            return new Resource
            {
                Data = new Dictionary<string, object>
                {
                    {  _resource.Data.TemplateTypeName, _resource.Data.Resource }
                },
                Meta = new Dictionary<string, object>
                {
                    {  _resource.Meta.TemplateTypeName, _resource.Meta.Properties.ToDictionary(
                        p => p.Name,
                        p => p.Constraints.ToDictionary(
                            c => c.Name,
                            c => c.Value
                        ))
                    }
                }
            };
        }
    }
}