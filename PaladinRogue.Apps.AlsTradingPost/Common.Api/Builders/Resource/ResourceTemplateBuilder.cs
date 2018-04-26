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

        private readonly IBuildHelper _buildHelper;

        public ResourceTemplateBuilder(IBuildHelper buildHelper)
        {
            _buildHelper = buildHelper;
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