using System.Collections.Generic;
using Common.Api.Links;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public class ResourceTemplateBuilder : IResourceTemplateBuilder
    {
        private ResourceBuilderResource<IResource> _resource;
        private ResourceBuilderResource<ITemplate> _template;

        private IResource _resourceData;
        private ITemplate _templateData;

        private readonly ILinkBuilder _linkBuilder;

        public ResourceTemplateBuilder(ILinkBuilder linkBuilder)
        {
            _linkBuilder = linkBuilder;
        }

        public IResourceTemplateBuilder Create(IResource resource, ITemplate template)
        {
            _resourceData = resource;
            _templateData = template;

            _resource = new ResourceBuilderResource<IResource>
            {
                Data = BuildHelper.BuildResourceData(_resourceData),
                Meta = BuildHelper.BuildMeta(_resourceData),
                Links = _linkBuilder.BuildLinks(_resourceData)
            };

            _template = new ResourceBuilderResource<ITemplate>
            {
                Data = BuildHelper.BuildResourceData(_templateData),
                Meta = BuildHelper.BuildMeta(_templateData)
            };

            return this;
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