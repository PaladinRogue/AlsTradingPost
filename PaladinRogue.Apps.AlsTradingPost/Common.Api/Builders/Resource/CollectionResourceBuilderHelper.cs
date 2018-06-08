using System.Collections.Generic;
using System.Linq;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public static class CollectionResourceBuilderHelper
    {
        public static IBuiltResource Build<T>(
            ResourceBuilderResource<ICollectionResource<T>> resource,
            ICollectionResource<T> resourceData,
            IList<ResourceBuilderResource<T>> collectionResources)
            where T : ISummaryResource
        {
            if (resourceData is IPagedCollectionResource<T> pagedCollectionResourceData)
            {
                return BuildHelper.Build(new ResourceBuilderResource<ResourceBuilderPagedCollectionResource>
                {
                    Data = new Data<ResourceBuilderPagedCollectionResource>
                    {
                        Resource = new ResourceBuilderPagedCollectionResource
                        {
                            Results = collectionResources.Select(BuildHelper.BuildCollectionItem),
                            TotalResults = pagedCollectionResourceData.TotalResults
                        },
                        TypeName = resource.Data.TypeName
                    },
                    Links = resource.Links,
                    Meta = resource.Meta
                });
            }

            return BuildHelper.Build(new ResourceBuilderResource<ResourceBuilderCollectionResource>
            {
                Data = new Data<ResourceBuilderCollectionResource>
                {
                    Resource = new ResourceBuilderCollectionResource
                    {
                        Results = collectionResources.Select(BuildHelper.BuildCollectionItem),
                    },
                    TypeName = resource.Data.TypeName
                },
                Links = resource.Links,
                Meta = resource.Meta
            });
        }
    }
}