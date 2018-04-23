using System.Collections.Generic;
using System.Linq;
using Common.Api.Builders.Dictionary;
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
        }

        public static CollectionResourceBuilder<T, TTemplate, TCollectionResource> Create(T resource,
            TTemplate template)
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
            IDictionaryBuilder<string, object> collectionResourceDataBuilder = DictionaryBuilder<string, object>
                .Create()
                .Add(nameof(_resourceData.Results), _collectionResources.Select(
                    r =>
                        DictionaryBuilder<string, object>.Create()
                            .Add(r.Data.TypeName, DictionaryBuilder<string, object>.Create()
                                .Add(ResourceType.Data, r.Data.Resource)
                                .Add(ResourceType.Links, r.Links.BuildLinkDictionary())
                                .Build())
                            .Build()
                ));

            if (_resourceData is IPagedCollectionResource<TCollectionResource> pagedCollectionResourceData)
            {
                collectionResourceDataBuilder.Add(nameof(pagedCollectionResourceData.TotalResults),
                    pagedCollectionResourceData.TotalResults);
            }


            IDictionaryBuilder<string, object> dictionaryBuilder = DictionaryBuilder<string, object>.Create()
                .Add(_resource.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Data, collectionResourceDataBuilder.Build())
                    .Add(ResourceType.Meta, _resource.Meta.Properties.BuildPropertyDictionary())
                    .Add(ResourceType.Links, _resource.Links.BuildLinkDictionary())
                    .Build())
                .Add(_template.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Data, _template.Data.Resource)
                    .Add(ResourceType.Meta, _template.Meta.Properties.BuildPropertyDictionary())
                    .Build());

            if (_collectionResources.Any())
            {
                dictionaryBuilder.Add(_collectionResources.First().Data.TypeName, DictionaryBuilder<string, object>
                    .Create()
                    .Add(ResourceType.Meta, _collectionResources.First().Meta.Properties.BuildPropertyDictionary())
                    .Build());
            }

            return dictionaryBuilder.Build();
        }
    }
}