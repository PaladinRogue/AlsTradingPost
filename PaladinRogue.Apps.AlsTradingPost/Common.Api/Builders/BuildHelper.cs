using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Api.Builders.Dictionary;
using Common.Api.Builders.Resource;
using Common.Api.Links;
using Common.Api.Meta;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;
using Common.Resources.Extensions;

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

        public static IDictionary<string, object> Build<T>(ResourceBuilderResource<T> resource)
        {
            return DictionaryBuilder<string, object>.Create()
                .Add(resource.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Data, resource.Data.Resource)
                    .Add(ResourceType.Meta, resource.Meta.Properties.BuildPropertyDictionary())
                    .Add(ResourceType.Links, resource.Links.BuildLinkDictionary())
                    .Build())
                .Build();
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
            return resourceName.Replace("Template", string.Empty).Replace("Resource", string.Empty);
        }
    }
}