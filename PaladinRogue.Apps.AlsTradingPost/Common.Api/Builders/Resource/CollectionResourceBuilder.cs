using System.Collections.Generic;
using System.Linq;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public class CollectionResourceBuilder<T, TTemplate, TCollectionResource> : ICollectionResourceBuilder
        where T : ICollectionResource<TCollectionResource>
        where TCollectionResource : ISummaryResource
    {
        private readonly ResourceBuilderResource<T> _resource;
        private readonly ResourceBuilderResource<TTemplate> _template;

        private readonly T _resourceData;
        private readonly TTemplate _templateData;
        private readonly IList<ResourceBuilderResource<TCollectionResource>> _collectionResources;

        private CollectionResourceBuilder(T resourceData, TTemplate templateData)
        {
            _resourceData = resourceData;
            _templateData = templateData;
            _collectionResources = BuildHelper.BuildCollectionResourceData(_resourceData);

            _resource = new ResourceBuilderResource<T>
            {
                Data = BuildHelper.BuildResourceData(_resourceData),
                Meta = BuildHelper.BuildMeta(_resourceData),
                Links = BuildHelper.BuildLinks(_resourceData)
            };

            _template = new ResourceBuilderResource<TTemplate>
            {
                Data = BuildHelper.BuildTemplateData(_templateData),
                Meta = BuildHelper.BuildMeta(_templateData)
            };

            BuildHelper.AddSelfQueryParams(_resource.Links, _templateData);
            
            if (_resourceData is IPagedCollectionResource<TCollectionResource> resource
                && _templateData is IPaginationTemplate paginationTemplate)
            {
                BuildHelper.AddPagingLinks(_resource, resource, paginationTemplate);
            }
        }

        public static CollectionResourceBuilder<T, TTemplate, TCollectionResource> Create(T resource, TTemplate template)
        {
            return new CollectionResourceBuilder<T, TTemplate, TCollectionResource>(resource, template);
        }

        public ICollectionResourceBuilder WithTemplateMeta()
        {
            BuildHelper.BuildValidationMeta(_template.Meta, _templateData);

            return this;
        }

        public ICollectionResourceBuilder WithResourceMeta()
        {
            BuildHelper.BuildFieldMeta(_resource.Meta, _resourceData);

            return this;
        }

        public ICollectionResourceBuilder WithSummaryResourceMeta()
        {
            if (_collectionResources.Any())
            {
                BuildHelper.BuildFieldMeta(_collectionResources.First().Meta, _resourceData.Results.First());
            }

            return this;
        }

        public ICollectionResourceBuilder WithSorting()
        {
            BuildHelper.BuildSortingMeta<TTemplate, TCollectionResource>(_template.Meta, _templateData);

            return this;
        }

        public IDictionary<string, object> Build()
        {
           return CollectionResourceBuilderHelper.Build(_resource, _resourceData, _template, _collectionResources);
        }
    }
}