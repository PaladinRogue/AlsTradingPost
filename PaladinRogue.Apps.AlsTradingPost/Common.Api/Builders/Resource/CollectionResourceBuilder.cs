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
                Meta = BuildHelper.BuildMeta(_templateData)
            };
        }

        public static CollectionResourceBuilder<T, TTemplate, TCollectionResource> Create(T resource,
            TTemplate template)
        {
            return new CollectionResourceBuilder<T, TTemplate, TCollectionResource>(resource, template);
        }

        public ICollectionResourceBuilder WithMeta(bool extendedMeta = false)
        {
            BuildHelper.BuildValidationMeta(_resource.Meta, _templateData);

            return this;
        }

        public ICollectionResourceBuilder WithResourceMeta()
        {
            BuildHelper.BuildFieldMeta(_resource.Meta, _resourceData);

            return this;
        }

        public ICollectionResourceBuilder WithSorting()
        {
            BuildHelper.BuildSortingMeta<TTemplate, TCollectionResource>(_resource.Meta, _templateData);

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
                                .Build())
                            .Build()
                ));

            if (_resourceData is IPagedCollectionResource<TCollectionResource> pagedCollectionResourceData)
            {
                collectionResourceDataBuilder.Add(nameof(pagedCollectionResourceData.TotalResults),
                    pagedCollectionResourceData.TotalResults);
            }


            return DictionaryBuilder<string, object>.Create()
                .Add(_resource.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Data, collectionResourceDataBuilder.Build())
                    .Add(ResourceType.Meta, DictionaryBuilder<string, object>.Create()
                        .Add(_resource.Meta.TemplateTypeName, _resource.Meta.Properties.ToDictionary(
                            p => p.Name,
                            p => p.Constraints.ToDictionary(
                                c => c.Name,
                                c => c.Value
                            )))
                        .Build())
                    .Build())
                .Build();
        }
    }
}