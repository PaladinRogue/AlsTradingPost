using System.Collections.Generic;
using System.Linq;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;
using Common.Api.Routing;
using Common.Api.Sorting;
using Common.Setup.Infrastructure.Constants;

namespace Common.Api.Links
{
    public class DefaultLinkFactory : ILinkFactory
    {
        private readonly IRouteProvider<bool> _routeProvider;
        private readonly IPagingLinkBuilder _pagingLinkBuilder;
        private readonly ISortingLinkBuilder _sortingLinkBuilder;

        public DefaultLinkFactory(
            IRouteProvider<bool> routeProvider,
            IPagingLinkBuilder pagingLinkBuilder,
            ISortingLinkBuilder sortingLinkBuilder)
        {
            _routeProvider = routeProvider;
            _pagingLinkBuilder = pagingLinkBuilder;
            _sortingLinkBuilder = sortingLinkBuilder;
        }

        public ILink Create(
            string linkName,
            string routeName,
            IEnumerable<AuthorisationLink> verbAuthorisationContextTypePairs,
            IResource resource,
            ITemplate template,
            string basePath = null)
        {
            HttpVerb allowVerbs = verbAuthorisationContextTypePairs.Select(a => a.HttpVerb)
                .Aggregate<HttpVerb, HttpVerb>(0, (current, f) => current | f);
            string uri = string.IsNullOrWhiteSpace(routeName) ? string.Empty : _routeProvider.GetRouteTemplate(routeName, true, resource);

            switch (resource)
            {
                case IPaginationTemplate paginationResourceTemplate when linkName == LinkType.Search:
                    return _pagingLinkBuilder.BuildLink(linkName, uri, paginationResourceTemplate);
                case ISortTemplate sortResourceTemplate when linkName == LinkType.Search:
                    return _sortingLinkBuilder.BuildLink(linkName, uri, sortResourceTemplate);
            }

            switch (template)
            {
                case IPaginationTemplate paginationTemplate when linkName == LinkType.Self:
                    return _pagingLinkBuilder.BuildLink(linkName, uri, paginationTemplate);
                case ISortTemplate sortTemplate when linkName == LinkType.Self:
                    return _sortingLinkBuilder.BuildLink(linkName, uri, sortTemplate);
            }

            return new Link
            {
                Name = linkName,
                AllowVerbs = allowVerbs,
                Uri = basePath == null ? uri : basePath + uri
            };
        }
    }
}
