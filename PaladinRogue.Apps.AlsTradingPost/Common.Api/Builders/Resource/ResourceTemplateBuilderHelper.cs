using System.Collections.Generic;
using Common.Api.Builders.Dictionary;

namespace Common.Api.Builders.Resource
{
    public static class ResourceTemplateBuilderHelper
    {
        public static IDictionary<string, object> Build<T, TTemplate>(
            ResourceBuilderResource<T> resource,
            ResourceBuilderResource<TTemplate> template)
        {
            return DictionaryBuilder<string, object>.Create()
                .Add(resource.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Data, resource.Data.Resource)
                    .Add(ResourceType.Meta, resource.Meta.Properties.BuildPropertyDictionary())
                    .Add(ResourceType.Links, resource.Links.BuildLinkDictionary())
                    .Build())
                .Add(template.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Meta, template.Meta.Properties.BuildPropertyDictionary())
                    .Build())
                .Build();
        }
    }
}