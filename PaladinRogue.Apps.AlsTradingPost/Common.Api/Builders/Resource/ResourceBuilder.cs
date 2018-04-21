using System.Collections.Generic;
using System.Linq;
using Common.Api.Builders.Dictionary;

namespace Common.Api.Builders.Resource
{
    public class ResourceBuilder<T, TTemplate> : IResourceBuilder
    {
        private readonly ResourceBuilderResource<T> _resource;

        private readonly T _resourceData;
        private readonly TTemplate _templateData;

        private ResourceBuilder(T resource, TTemplate template)
        {
            _resourceData = resource;
            _templateData = template;

            _resource = new ResourceBuilderResource<T>
            {
                Data = BuildHelper.BuildResourceData(resource),
                Meta = BuildHelper.BuildMeta(_templateData)
            };
        }

        public static ResourceBuilder<T, TTemplate> Create(T resource, TTemplate template)
        {
            return new ResourceBuilder<T, TTemplate>(resource, template);
        }

        public IResourceBuilder WithMeta(bool extendedMeta = false)
        {
            BuildHelper.BuildValidationMeta(_resource.Meta, _templateData);

            return this;
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