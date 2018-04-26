using System.Collections.Generic;
using Common.Api.Links;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public class ResourceBuilder : IResourceBuilder
    {
        private ResourceBuilderResource<IResource> _resource;
        
        private IResource _resourceData;
        
        private readonly ILinkBuilder _linkBuilder;

        public ResourceBuilder(ILinkBuilder linkBuilder)
        {
            _linkBuilder = linkBuilder;
        }

        public IResourceBuilder Create(IResource resource)
        {
            _resourceData = resource;

            _resource = new ResourceBuilderResource<IResource>
            {
                Data = BuildHelper.BuildResourceData(_resourceData),
                Meta = BuildHelper.BuildMeta(_resourceData),
                Links = _linkBuilder.BuildLinks(_resourceData)
            };

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