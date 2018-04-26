using System.Collections.Generic;
using System.Linq;
using Common.Api.Links;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public class CollectionResourceBuilder<T> : ICollectionResourceBuilder<T> where T : ISummaryResource
    {
        private ResourceBuilderResource<ICollectionResource<T>> _resource;
        private ResourceBuilderResource<ITemplate> _template;

        private ICollectionResource<T> _resourceData;
        private ITemplate _templateData;
        private IList<ResourceBuilderResource<T>> _collectionResources;

        private readonly IBuildHelper _buildHelper;

        public CollectionResourceBuilder(IBuildHelper buildHelper)
        {
            _buildHelper = buildHelper;
        }

        public ICollectionResourceBuilder<T> Create(ICollectionResource<T> collectionResource, ITemplate template)
        {
            _resourceData = collectionResource;
            _templateData = template;
            _collectionResources = _buildHelper.BuildCollectionResourceData(_resourceData);

            _resource = _buildHelper.BuildResourceBuilder(_resourceData);
            
            _template = _buildHelper.BuildResourceBuilder(_templateData);

            BuildHelper.AddSelfQueryParams(_resource.Links, _templateData);
            
            if (_resourceData is IPagedCollectionResource<T> resource
                && _templateData is IPaginationTemplate paginationTemplate)
            {
                BuildHelper.AddPagingLinks(_resource, resource, paginationTemplate);
            }

            return this;
        }

        public ICollectionResourceBuilder<T> WithTemplateMeta()
        {
            BuildHelper.BuildValidationMeta(_template.Meta, _templateData);

            return this;
        }

        public ICollectionResourceBuilder<T> WithResourceMeta()
        {
            BuildHelper.BuildFieldMeta(_resource.Meta, _resourceData);

            return this;
        }

        public ICollectionResourceBuilder<T> WithSummaryResourceMeta()
        {
            if (_collectionResources.Any() && _resourceData.Results.Any())
            {
                BuildHelper.BuildFieldMeta(_collectionResources.First().Meta, _resourceData.Results.First());
            }

            return this;
        }

        public ICollectionResourceBuilder<T> WithSorting()
        {
            if (_resourceData.Results.Any())
            {
                BuildHelper.BuildSortingMeta(_template.Meta, _templateData, _resourceData.Results.First().GetType());
            }

            return this;
        }

        public IDictionary<string, object> Build()
        {
           return CollectionResourceBuilderHelper.Build(_resource, _resourceData, _template, _collectionResources);
        }
    }
}