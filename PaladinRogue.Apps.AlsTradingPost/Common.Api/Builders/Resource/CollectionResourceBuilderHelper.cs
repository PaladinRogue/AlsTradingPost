using System.Collections.Generic;
using System.Linq;
using Common.Api.Builders.Dictionary;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public static class CollectionResourceBuilderHelper
    {

        public static IDictionary<string, object> Build<T, TTemplate, TSummaryResource>(
            ResourceBuilderResource<T> resource,
            ICollectionResource<TSummaryResource> resourceData,
            ResourceBuilderResource<TTemplate> template,
            IList<ResourceBuilderResource<TSummaryResource>> collectionResources)
            where TSummaryResource : ISummaryResource
        {
            IDictionaryBuilder<string, object> collectionResourceDataBuilder = DictionaryBuilder<string, object>
                .Create()
                .Add(nameof(resourceData.Results), collectionResources.Select(
                    r =>
                        DictionaryBuilder<string, object>.Create()
                            .Add(r.Data.TypeName, DictionaryBuilder<string, object>.Create()
                                .Add(ResourceType.Data, r.Data.Resource)
                                .Add(ResourceType.Links, r.Links.BuildLinkDictionary())
                                .Build())
                            .Build()
                ));

            if (resourceData is IPagedCollectionResource<TSummaryResource> pagedCollectionResourceData)
            {
                collectionResourceDataBuilder.Add(nameof(pagedCollectionResourceData.TotalResults),
                    pagedCollectionResourceData.TotalResults);
            }


            IDictionaryBuilder<string, object> dictionaryBuilder = DictionaryBuilder<string, object>.Create()
                .Add(resource.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Data, collectionResourceDataBuilder.Build())
                    .Add(ResourceType.Meta, resource.Meta.Properties.BuildPropertyDictionary())
                    .Add(ResourceType.Links, resource.Links.BuildLinkDictionary())
                    .Build())
                .Add(template.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Data, template.Data.Resource)
                    .Add(ResourceType.Meta, template.Meta.Properties.BuildPropertyDictionary())
                    .Build());

            if (collectionResources.Any())
            {
                dictionaryBuilder.Add(collectionResources.First().Data.TypeName, DictionaryBuilder<string, object>
                    .Create()
                    .Add(ResourceType.Meta, collectionResources.First().Meta.Properties.BuildPropertyDictionary())
                    .Build());
            }

            return dictionaryBuilder.Build();
        }
    }
}