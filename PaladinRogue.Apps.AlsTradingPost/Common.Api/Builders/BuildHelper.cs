using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Common.Api.Builders.Dictionary;
using Common.Api.Builders.Resource;
using Common.Api.Links;
using Common.Api.Meta;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;
using Common.Resources.Extensions;
using Newtonsoft.Json;

namespace Common.Api.Builders
{
    public class BuildHelper : IBuildHelper
    {
        private readonly ILinkBuilder _linkBuilder;
        private readonly IMetaBuilder _metaBuilder;
        
        public BuildHelper(ILinkBuilder linkBuilder, IMetaBuilder metaBuilder)
        {
            _linkBuilder = linkBuilder;
            _metaBuilder = metaBuilder;
        }
        
        public IList<ResourceBuilderResource<TSummaryResource>> BuildCollectionResourceData<TSummaryResource>(ICollectionResource<TSummaryResource> data)
            where TSummaryResource : ISummaryResource
        {
            return data.Results.Select(BuildResourceBuilder).ToList();
        }

        public ResourceBuilderResource<T> BuildResourceBuilder<T>(T data)
        {
            return new ResourceBuilderResource<T>
            {
                Data = BuildData(data),
                Meta = _metaBuilder.BuildMeta(data),
                Links = _linkBuilder.BuildLinks(data)
            };
        }

        public static void AddSearchQueryParams<T>(IEnumerable<Link> links, T data)
        {
            AddQueryParams(links.Single(l => l.Name == LinkType.Search), data);
        }

        public static void AddSelfQueryParams<T>(IEnumerable<Link> links, T data)
        {
            AddQueryParams(links.Single(l => l.Name == LinkType.Self), data);
        }

        public static void AddPagingLinks<T, TSummaryResource>(ResourceBuilderResource<T> resource, IPagedCollectionResource<TSummaryResource> data, IPaginationTemplate pagingData) 
            where TSummaryResource : ISummaryResource
        {
            Link selfLink = resource.Links.Single(l => l.Name == LinkType.Self);
            
            foreach (Link pagingLink in PagingLinkHelper.GetPagingLinks(pagingData, data.TotalResults))
            {
                pagingLink.Uri = selfLink.Uri;
                resource.Links.Add(pagingLink);
            }
        }

        public static IBuiltResource Build<T>(ResourceBuilderResource<T> resource)
        {
            IBuiltResource builtResource = new BuiltResource
            {
                Data = BuildResourceData(resource.Data),
                Meta = resource.Meta.Properties.BuildPropertyDictionary(),
                Links = resource.Links.BuildLinkDictionary()
            };

            return builtResource;
        }

        public static IBuiltResource BuildCollectionItem<T>(ResourceBuilderResource<T> resource)
        {
            IBuiltResource builtResource = new BuiltCollectionResource
            {
                Data = BuildResourceData(resource.Data),
                Links = resource.Links.BuildLinkDictionary()
            };

            return builtResource;
        }

        private static object BuildResourceData<T>(Data<T> resourceData)
        {
            DictionaryBuilder<string, object> resourceBuilder = DictionaryBuilder<string, object>.Create();

            foreach (PropertyInfo propertyInfo in resourceData.Resource.GetType().GetProperties())
            {
                if (!propertyInfo.GetCustomAttributes<JsonIgnoreAttribute>().Any())
                {
                    resourceBuilder.Add(propertyInfo.Name, propertyInfo.GetValue(resourceData.Resource));
                }
            }

            resourceBuilder.Add(ResourceType.Type, resourceData.TypeName.ToCamelCase());

            return resourceBuilder.Build();
        }

        private static Data<T> BuildData<T>(T resourceData)
        {
            return new Data<T>
            {
                TypeName = GetConventionTypeName(resourceData),
                Resource = resourceData
            };
        }

        private static void AddQueryParams<T>(Link link, T data)
        {
            if (link == null)
            {
                return;
            }
            
            link.QueryParams = data.GetType().GetProperties()
                .Where(p => p.GetValue(data) != null)
                .ToDictionary(
                    p => p.Name.ToCamelCase(),
                    p => p.GetValue(data).ToString().ToCamelCase()
                );
        }
        
        private static string GetConventionTypeName<T>(T data)
        {
            NameAttribute nameAttribute = data.GetType().GetCustomAttribute<NameAttribute>();
            return nameAttribute?.Name ?? FormatConventionName(data.GetType().Name);
        }
        
        private static string FormatConventionName(string resourceName)
        {
            resourceName = Regex.Replace(resourceName, @"Template$", string.Empty);
            resourceName = Regex.Replace(resourceName, @"Resource$", string.Empty);

            return resourceName;
        }
    }
}