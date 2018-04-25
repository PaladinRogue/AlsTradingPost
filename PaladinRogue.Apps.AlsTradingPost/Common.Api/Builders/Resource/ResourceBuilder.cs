using System.Collections.Generic;
using Common.Api.Links;

namespace Common.Api.Builders.Resource
{
    public class ResourceBuilder<T> : IResourceBuilder<T>
    {
        private ResourceBuilderResource<T> _resource;
        
        private T _resourceData;
        
        private readonly ILinkBuilder _linkBuilder;

        public ResourceBuilder(ILinkBuilder linkBuilder)
        {
            _linkBuilder = linkBuilder;
        }

        public IResourceBuilder<T> Create(T resource)
        {
            _resourceData = resource;

            _resource = new ResourceBuilderResource<T>
            {
                Data = BuildHelper.BuildResourceData(_resourceData),
                Meta = BuildHelper.BuildMeta(_resourceData),
                Links = _linkBuilder.BuildLinks(_resourceData)
            };

            return this;
        }

        public IResourceBuilder<T> WithResourceMeta()
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