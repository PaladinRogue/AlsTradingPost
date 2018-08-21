using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Api.Pagination;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;

namespace Common.Api.Links
{
    public class LinkBuilder : ILinkBuilder
    {
        private readonly ILinkFactory _linkFactory;
        private readonly IPagingLinkBuilder _pagingLinkBuilder;

        public LinkBuilder(ILinkFactory linkFactory, IPagingLinkBuilder pagingLinkBuilder)
        {
            _linkFactory = linkFactory;
            _pagingLinkBuilder = pagingLinkBuilder;
        }

        public Links BuildLinks(IResource resource, ITemplate template = null)
        {
            IList<ILink> links = resource.GetType().GetCustomAttributes<LinkAttribute>()
                .GroupBy(linkAttribute => new
                {
                    linkAttribute.LinkName,
                    linkAttribute.UriName
                })
                .Select(linkAttributeGrouping => _linkFactory.Create(
                    linkAttributeGrouping.Key.LinkName,
                    linkAttributeGrouping.Key.UriName,
                    linkAttributeGrouping.Select(linkAttribute => AuthorisationLink.Create(linkAttribute.HttpVerb, linkAttribute.AuthorisationContextType)),
                    resource,
                    template)
                )
                .ToList();

            ILink selfLink = links.FirstOrDefault(l => l.Name == LinkType.Self);

            return new Links
            {
                Self = selfLink,
                RelatedLinks = links.Where(l => l.Name != LinkType.Self),
                PagingLinks = selfLink == null ? null : _pagingLinkBuilder.BuildPagingLinks(template as IPaginationTemplate, selfLink, (resource as IPagedResource)?.TotalResults)
            };
        }
    }
}