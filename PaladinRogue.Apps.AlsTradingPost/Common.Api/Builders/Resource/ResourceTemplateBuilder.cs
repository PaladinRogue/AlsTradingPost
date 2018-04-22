using System.Collections.Generic;
using Common.Api.Builders.Dictionary;

namespace Common.Api.Builders.Resource
{
    public class ResourceTemplateBuilder<T, TTemplate> : IResourceTemplateBuilder
    {
        private readonly ResourceBuilderResource<T> _resource;
        private readonly ResourceBuilderResource<TTemplate> _template;

        private readonly T _resourceData;
        private readonly TTemplate _templateData;

        private ResourceTemplateBuilder(T resource, TTemplate template)
        {
            _resourceData = resource;
            _templateData = template;

            _resource = new ResourceBuilderResource<T>
            {
                Data = BuildHelper.BuildResourceData(_resourceData),
                Meta = BuildHelper.BuildMeta(_resourceData),
                Links = BuildHelper.BuildLinks(_resourceData)
            };

            _template = new ResourceBuilderResource<TTemplate>
            {
                Data = BuildHelper.BuildResourceData(_templateData),
                Meta = BuildHelper.BuildMeta(_templateData)
            };
        }

        public static ResourceTemplateBuilder<T, TTemplate> Create(T resource, TTemplate template)
        {
            return new ResourceTemplateBuilder<T, TTemplate>(resource, template);
        }

        public IResourceTemplateBuilder WithTemplateMeta()
        {
            BuildHelper.BuildValidationMeta(_template.Meta, _templateData);

            return this;
        }

        public IResourceTemplateBuilder WithResourceMeta()
        {
            BuildHelper.BuildFieldMeta(_resource.Meta, _resourceData);

            return this;
        }

        public IDictionary<string, object> Build()
        {
            return DictionaryBuilder<string, object>.Create()
                .Add(_resource.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Data, _resource.Data.Resource)
                    .Add(ResourceType.Meta, _resource.Meta.Properties.BuildPropertyDictionary())
                    .Add(ResourceType.Links, _resource.Links.BuildLinkDictionary())
                    .Build())
                .Add(_template.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Meta, _template.Meta.Properties.BuildPropertyDictionary())
                    .Build())
                .Build();
        }
    }
}