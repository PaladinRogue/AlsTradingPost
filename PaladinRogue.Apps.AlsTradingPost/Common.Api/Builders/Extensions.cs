using System.Collections.Generic;
using System.Linq;
using Common.Api.Builders.Dictionary;
using Common.Api.Links;
using Common.Api.Meta;

namespace Common.Api.Builders
{
    public static class Extensions
    {
        public static IDictionary<string, Dictionary<string, object>> BuildPropertyDictionary(
            this IEnumerable<PropertyMeta> propertyMetas)
        {
            return propertyMetas.Where(p => p.Constraints.Any()).ToDictionary(
                p => p.Name,
                p => p.Constraints.ToDictionary(
                    c => c.Name,
                    c => c.Value
                )
            );
        }

        public static IDictionary<string, IDictionary<string, object>> BuildLinkDictionary(this IEnumerable<Link> links)
        {
            return links.ToDictionary(
                p => p.Name,
                p => DictionaryBuilder<string, object>.Create()
                    .Add(LinkPartType.Href, p.FullUri)
                    .Add(LinkPartType.AllowVerbs, p.AllowVerbs)
                    .Build()
            );
        }
    }
}