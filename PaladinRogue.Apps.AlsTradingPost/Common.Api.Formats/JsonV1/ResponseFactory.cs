using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Common.Api.Builders.Dictionary;
using Common.Api.Builders.Resource;
using Common.Api.Formats.JsonV1.Formats;
using Common.Api.Headers;
using Common.Api.Links;
using Common.Resources.Extensions;
using Microsoft.AspNetCore.Http;
using Link = Common.Api.Formats.JsonV1.Formats.Link;

namespace Common.Api.Formats.JsonV1
{
    public class ResponseFactory
    {
        public static FormattedResponse Create(BuiltResource builtResource, HttpRequest httpRequest)
        {
            bool includeExtendedMeta = httpRequest.Headers.ContainsKey(HeaderType.ExtendedMeta);

            DictionaryBuilder<string, object> attributeBuilder = DictionaryBuilder<string, object>.Create();

            foreach (Property builtResourceProperty in builtResource.Properties)
            {
                attributeBuilder.Add(builtResourceProperty.Name, builtResourceProperty.Value);
            }

            return new FormattedResponse
            {
                Data = new Data
                {
                    Type = _formatTypeName(builtResource.Type),
                    Id = builtResource.Id,
                    Attributes = attributeBuilder.Build(),
                    Meta = _buildMeta(builtResource.Properties, includeExtendedMeta)
                },
                Links = _buildLinks(builtResource.Links)
            };
        }

        public static FormattedArrayResponse Create(BuiltCollectionResource builtCollectionResource, HttpRequest httpRequest)
        {
            bool includeExtendedMeta = httpRequest.Headers.ContainsKey(HeaderType.ExtendedMeta);

            DictionaryBuilder<string, object> metaBuilder = DictionaryBuilder<string, object>.Create();

            metaBuilder.Add(MetaType.TotalResults, builtCollectionResource.TotalResults);

            if (builtCollectionResource.BuiltResources.Any(
                r => r.Properties.Any(p => p.Constraints.IsSortable ?? false)))
            {
                DictionaryBuilder<string, object> sortBuilder = DictionaryBuilder<string, object>.Create();
                sortBuilder.Add(MetaType.Values, builtCollectionResource.BuiltResources.First().Properties.Where(p => p.Constraints.IsSortable ?? false).Select(p => p.Name.ToCamelCase()));

                metaBuilder.Add(MetaType.Sort, sortBuilder.Build());
            }

            return new FormattedArrayResponse
            {
                Data = builtCollectionResource.BuiltResources.Select(r =>
                    {
                        DictionaryBuilder<string, object> attributeBuilder = DictionaryBuilder<string, object>.Create();

                        foreach (Property builtResourceProperty in r.Properties)
                        {
                            attributeBuilder.Add(builtResourceProperty.Name, builtResourceProperty.Value);
                        }

                        return new Data
                        {
                            Type = _formatTypeName(r.Type),
                            Id = r.Id,
                            Attributes = attributeBuilder.Build(),
                            Links = _buildLinks(r.Links),
                            Meta = _buildMeta(r.Properties, includeExtendedMeta)
                        };
                    }
                ),
                Links = _buildLinks(builtCollectionResource.Links),
                Meta = metaBuilder.Build()
            };
        }

        private static IDictionary<string, object> _buildMeta(IEnumerable<Property> properties, bool includeExtendedMeta)
        {
            DictionaryBuilder<string, object> metaBuilder = DictionaryBuilder<string, object>.Create();

            foreach (Property property in properties)
            {
                metaBuilder.Add(property.Name, _buildPropertyMeta(property, includeExtendedMeta));
            }

            return metaBuilder.Build();
        }

        private static IDictionary<string, object> _buildPropertyMeta(Property property, bool includeExtendedMeta)
        {
            DictionaryBuilder<string, object> propertyMetaBuilder = DictionaryBuilder<string, object>.Create();

            propertyMetaBuilder.Add(MetaType.Type, FieldTypeMapper.GetFieldType(property.Type));

            if (property.Constraints != null)
            {
                if (property.Constraints.IsHidden.HasValue)
                {
                    propertyMetaBuilder.Add(MetaType.IsHidden, property.Constraints.IsHidden);
                }

                if (property.Constraints.IsReadonly.HasValue)
                {
                    propertyMetaBuilder.Add(MetaType.IsReadonly, property.Constraints.IsReadonly);
                }
            }

            if (property.ValidationConstraints != null && includeExtendedMeta)
            {
                if (property.ValidationConstraints.IsRequired.HasValue)
                {
                    propertyMetaBuilder.Add(MetaType.IsRequired, property.ValidationConstraints.IsRequired);
                }

                if (property.ValidationConstraints.MaxLength.HasValue)
                {
                    propertyMetaBuilder.Add(MetaType.MaxLength, property.ValidationConstraints.MaxLength);
                }

                if (property.ValidationConstraints.MinLenth.HasValue)
                {
                    propertyMetaBuilder.Add(MetaType.MinLenth, property.ValidationConstraints.MinLenth);
                }
            }
            
            return propertyMetaBuilder.Build();
        }

        private static IDictionary<string, Link> _buildLinks(Links.Links links)
        {
            DictionaryBuilder<string, Link> linkBuilder = DictionaryBuilder<string, Link>.Create();

            if (links.Self != null)
            {
                linkBuilder.Add(links.Self.Name, LinkFactory.Create(links.Self));
            }

            if (links.PagingLinks != null)
            {
                if (links.PagingLinks.First != null)
                {
                    linkBuilder.Add(links.PagingLinks.First.Name, LinkFactory.Create(links.PagingLinks.First));
                }

                if (links.PagingLinks.Previous != null)
                {
                    linkBuilder.Add(links.PagingLinks.Previous.Name, LinkFactory.Create(links.PagingLinks.Previous));
                }

                if (links.PagingLinks.Next != null)
                {
                    linkBuilder.Add(links.PagingLinks.Next.Name, LinkFactory.Create(links.PagingLinks.Next));
                }

                if (links.PagingLinks.Last != null)
                {
                    linkBuilder.Add(links.PagingLinks.Last.Name, LinkFactory.Create(links.PagingLinks.Last));
                }
            }

            if (links.RelatedLinks != null)
            {
                foreach (ILink relatedLink in links.RelatedLinks)
                {
                    linkBuilder.Add(relatedLink.Name, LinkFactory.Create(relatedLink));
                }
            }

            return linkBuilder.Build();
        }

        private static string _formatTypeName(MemberInfo type)
        {
            Regex regex = new Regex(@"(resource|template)$", RegexOptions.IgnoreCase);

            return regex.Replace(type.Name, string.Empty).ToCamelCase();
        }
    }
}
