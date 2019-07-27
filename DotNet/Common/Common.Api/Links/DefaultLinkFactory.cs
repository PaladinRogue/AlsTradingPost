using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;
using Common.Api.Routing;
using Common.Api.Sorting;
using Common.Authorisation.Contexts;
using Common.Authorisation.Policies;
using Common.Setup.Infrastructure.Constants;

namespace Common.Api.Links
{
    public class DefaultLinkFactory : ILinkFactory
    {
        private readonly IRouteProvider<bool> _routeProvider;

        private readonly IAuthorisationPolicy _authorisationPolicy;

        private readonly IPagingLinkBuilder _pagingLinkBuilder;

        private readonly ISortingLinkBuilder _sortingLinkBuilder;

        public DefaultLinkFactory(
            IRouteProvider<bool> routeProvider,
            IPagingLinkBuilder pagingLinkBuilder,
            ISortingLinkBuilder sortingLinkBuilder,
            IAuthorisationPolicy authorisationPolicy)
        {
            _routeProvider = routeProvider;
            _pagingLinkBuilder = pagingLinkBuilder;
            _sortingLinkBuilder = sortingLinkBuilder;
            _authorisationPolicy = authorisationPolicy;
        }

        public ILink Create<TResource, TTemplate>(
            string linkName,
            string routeName,
            IEnumerable<AuthorisationLink> verbAuthorisationContextTypePairs,
            TResource resource,
            TTemplate template,
            string basePath = null)
        {
            HttpVerb allowVerbs = verbAuthorisationContextTypePairs
                .Where(linkAttribute =>
                {
                    if (linkAttribute.AuthorisationContextType == null)
                    {
                        return true;
                    }

                    IAuthorisationContext authorisationContext;
                    if (typeof(IEntityResource).IsAssignableFrom(typeof(TResource)))
                    {
                        Guid resourceId = ((IEntityResource) resource).Id;
                        authorisationContext = (IAuthorisationContext) Activator.CreateInstance(linkAttribute.AuthorisationContextType, resourceId);
                    }
                    else if (typeof(IEntityResource).IsAssignableFrom(typeof(TTemplate)))
                    {
                        Guid resourceId = ((IEntityResource) template).Id;
                        authorisationContext = (IAuthorisationContext) Activator.CreateInstance(linkAttribute.AuthorisationContextType, resourceId);
                    }
                    else
                    {
                        authorisationContext = (IAuthorisationContext) Activator.CreateInstance(linkAttribute.AuthorisationContextType);
                    }

                    return _authorisationPolicy.HasAccessAsync(authorisationContext).Result;
                })
                .Select(a => a.HttpVerb)
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
