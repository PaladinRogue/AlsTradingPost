using System.Collections.Generic;
using Common.Api.Meta;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public class ResourceBuilder : IResourceBuilder
    {
        private ResourceBuilderResource<IResource> _resource;
        
        private IResource _resourceData;
        
        private readonly IBuildHelper _buildHelper;
        private readonly IMetaBuilder _metaBuilder;

        public ResourceBuilder(IBuildHelper buildHelper, IMetaBuilder metaBuilder)
        {
            _buildHelper = buildHelper;
            _metaBuilder = metaBuilder;
        }

        public IResourceBuilder Create(IResource resource)
        {
            _resourceData = resource;

            _resource = _buildHelper.BuildResourceBuilder(_resourceData);

            return this;
        }

        public IResourceBuilder WithResourceMeta()
        {
            _metaBuilder.BuildFieldMeta(_resource.Meta, _resourceData);

            return this;
        }

        public IDictionary<string, object> Build()
        {
            return BuildHelper.Build(_resource);
        }
    }
}