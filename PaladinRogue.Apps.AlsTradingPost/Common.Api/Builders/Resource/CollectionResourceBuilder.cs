using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public class CollectionResourceBuilder<T, TTemplate, TCollectionResource> : ResourceBuilder<T, TTemplate>, ICollectionResourceBuilder 
        where T : IPagedCollectionResource<TCollectionResource>
        where TCollectionResource : ISummaryResource
    {
        private readonly IList<ResourceBuilderResource<TCollectionResource>> _collectionResources;

        private CollectionResourceBuilder(T resource, TTemplate template)
            : base(resource, template)
        {
            _collectionResources = BuildHelper.BuildCollectionResourceData(ResourceData);
        }

        public new static ICollectionResourceBuilder Create(T resource, TTemplate template)
        {
            return new CollectionResourceBuilder<T, TTemplate, TCollectionResource>(resource, template);
        }

        public ICollectionResourceBuilder WithSorting()
        {
            if (Resource.Meta == null)
            {
                throw new ArgumentException("You must build the meta before adding sorting");
            }

            BuildHelper.AddSorting<TTemplate, TCollectionResource>(Resource.Meta, Template);

            return this;
        }

        public new IDictionary<string, object> Build()
        {
            return new Dictionary<string, object>
            {
                {
                    ResourceType.Data, new Dictionary<string, object>
                    {
                        {
                            Resource.Data.TypeName, new Dictionary<string, object>
                            {
                                { nameof(Resource.Data.Resource.TotalResults), Resource.Data.Resource.TotalResults },
                                { ResourceType.Results, _collectionResources }
                            }
                        }
                    }
                },
                {
                    ResourceType.Meta, new Dictionary<string, object>
                    {
                        {
                            Resource.Meta.TemplateTypeName, Resource.Meta.Properties.ToDictionary(
                                p => p.Name,
                                p => p.Constraints.ToDictionary(
                                    c => c.Name,
                                    c => c.Value
                                ))
                        }
                    }
                }
            };
        }
    }

    public interface ICollectionResourceBuilder : IResourceBuilder
    {
        ICollectionResourceBuilder WithSorting();
    }
}