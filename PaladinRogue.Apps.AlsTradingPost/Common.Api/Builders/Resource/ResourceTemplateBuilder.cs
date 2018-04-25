﻿using System.Collections.Generic;

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
            return ResourceTemplateBuilderHelper.Build(_resource, _template);
        }
    }
}