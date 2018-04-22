using System.Collections.Generic;
using System.Linq;
using Common.Api.Builders.Dictionary;

namespace Common.Api.Builders.Resource
{
    public class ResourceBuilder<T> : IResourceBuilder
    {
        private readonly ResourceBuilderResource<T> _resource;

        private readonly T _resourceData;
        
        private ResourceBuilder(T resource)
        {
            _resourceData = resource;

            _resource = new ResourceBuilderResource<T>
            {
                Data = BuildHelper.BuildResourceData(_resourceData),
                Meta = BuildHelper.BuildMeta(_resourceData)
            };
        }

        public static ResourceBuilder<T> Create(T resource)
        {
            return new ResourceBuilder<T>(resource);
        }

        public IResourceBuilder WithResourceMeta()
        {
            BuildHelper.BuildFieldMeta(_resource.Meta, _resourceData);

            return this;
        }

        public IDictionary<string, object> Build()
        {
            return DictionaryBuilder<string, object>.Create()
                .Add(_resource.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Data, _resource.Data.Resource)
                    .Add(ResourceType.Meta, _resource.Meta.Properties.ToDictionary(
                        p => p.Name,
                        p => p.Constraints.ToDictionary(
                            c => c.Name,
                            c => c.Value
                        )))
                    .Build())
                .Build();
        }
    }
}