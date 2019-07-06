using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Api.Pagination;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Links
{
    public class DefaultLinkBuilder : ILinkBuilder
    {
        private readonly ILinkFactory _linkFactory;
        private readonly IPagingLinkBuilder _pagingLinkBuilder;
        private readonly IServiceProvider _services;

        public DefaultLinkBuilder(
            ILinkFactory linkFactory,
            IPagingLinkBuilder pagingLinkBuilder,
            IServiceProvider services)
        {
            _linkFactory = linkFactory;
            _pagingLinkBuilder = pagingLinkBuilder;
            _services = services;
        }

        public Links BuildLinks<TResource>(TResource resource) where TResource : IResource
        {
            return BuildLinks<TResource, ITemplate>(resource, null);
        }

        public Links BuildLinks<TResource, TTemplate>(TResource resource, TTemplate template) where TResource : IResource where TTemplate : IResource
        {
            IList<ILink> links = resource.GetType().GetCustomAttributes<LinkAttribute>()
                .GroupBy(linkAttribute => new
                {
                    Attribute = linkAttribute,
                    linkAttribute.LinkName,
                    linkAttribute.UriName
                })
                .Select(linkAttributeGrouping =>
                {
                    string basePath = string.Empty;

                    if (linkAttributeGrouping.Key.Attribute is AbsoluteLinkAttribute absoluteLinkAttribute)
                    {
                        IAbsoluteLinkProvider absoluteLinkProvider = (IAbsoluteLinkProvider)_services.GetRequiredService(absoluteLinkAttribute.AbsoluteLinkProviderType);
                        basePath = absoluteLinkProvider.GetAbsoluteUrl();
                    }

                    return _linkFactory.Create(
                        linkAttributeGrouping.Key.LinkName,
                        linkAttributeGrouping.Key.UriName,
                        linkAttributeGrouping.Select(linkAttribute =>
                            AuthorisationLink.Create(linkAttribute.HttpVerb, linkAttribute.AuthorisationContextType)),
                        resource,
                        template,
                        basePath);
                })
                .ToList();

            ILink selfLink = links.FirstOrDefault(l => l.Name == LinkType.Self);

            IEnumerable<DynamicLinksAttribute> dynamicLinkAttributes = resource.GetType().GetCustomAttributes<DynamicLinksAttribute>();

            IEnumerable<ILink> dynamicLinks = dynamicLinkAttributes.SelectMany(d =>
            {
                IDynamicLinksProvider dynamicLinksProvider =
                    (IDynamicLinksProvider) _services.GetRequiredService(d.DynamicLinksProviderType);

                return dynamicLinksProvider.GetLinks();
            });

            return new Links
            {
                Self = selfLink,
                RelatedLinks = links.Where(l => l.Name != LinkType.Self).Concat(dynamicLinks),
                PagingLinks = selfLink == null ? null : _pagingLinkBuilder.BuildPagingLinks(template as IPaginationTemplate, selfLink, (resource as IPagedResource)?.TotalResults)
            };
        }
    }
}