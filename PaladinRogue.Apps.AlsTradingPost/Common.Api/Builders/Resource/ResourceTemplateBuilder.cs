using System.Collections.Generic;
using Common.Api.Meta;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public class ResourceTemplateBuilder : IResourceTemplateBuilder
    {
        private ResourceBuilderResource<IResource> _resource;
        private ResourceBuilderResource<ITemplate> _template;

        private IResource _resourceData;
        private ITemplate _templateData;

        private readonly IBuildHelper _buildHelper;
        private readonly IMetaBuilder _metaBuilder;

        public ResourceTemplateBuilder(IBuildHelper buildHelper, IMetaBuilder metaBuilder)
        {
            _buildHelper = buildHelper;
            _metaBuilder = metaBuilder;
        }

        public IResourceTemplateBuilder Create(IResource resource, ITemplate template)
        {
            _resourceData = resource;
            _templateData = template;

            _resource = _buildHelper.BuildResourceBuilder(_resourceData);
            
            _template = _buildHelper.BuildResourceBuilder(_templateData);

            return this;
        }

        public IResourceTemplateBuilder WithTemplateMeta()
        {
            _metaBuilder.BuildValidationMeta(_template.Meta, _templateData);

            return this;
        }

        public IResourceTemplateBuilder WithResourceMeta()
        {
            _metaBuilder.BuildFieldMeta(_resource.Meta, _resourceData);

            return this;
        }

        public IDictionary<string, object> Build()
        {
            return ResourceTemplateBuilderHelper.Build(_resource, _template);
        }
    }
}