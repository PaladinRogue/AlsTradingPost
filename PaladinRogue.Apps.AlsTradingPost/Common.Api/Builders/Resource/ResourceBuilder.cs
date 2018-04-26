using System.Collections.Generic;
using Common.Api.Links;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public class ResourceBuilder : IResourceBuilder
    {
        private ResourceBuilderResource<IResource> _resource;
        
        private IResource _resourceData;
        
        private readonly IBuildHelper _buildHelper;

        public ResourceBuilder(IBuildHelper buildHelper)
        {
            _buildHelper = buildHelper;
        }

        public IResourceBuilder Create(IResource resource)
        {
            _resourceData = resource;

            _resource = _buildHelper.BuildResourceBuilder(_resourceData);

            return this;
        }

        public IResourceBuilder WithResourceMeta()
        {
            BuildHelper.BuildFieldMeta(_resource.Meta, _resourceData);

            return this;
        }

        public IDictionary<string, object> Build()
        {
            return BuildHelper.Build(_resource);
        }
    }
}