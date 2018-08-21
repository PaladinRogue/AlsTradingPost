using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Resources;
using AlsTradingPost.Setup.Infrastructure.Authorisation;
using Common.Api.Links;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;
using Common.Api.Routing;
using Common.Api.Sorting;
using Common.Application.Authorisation;
using Common.Setup.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;

namespace AlsTradingPost.Setup.Infrastructure.Links
{
    public class PersonaLinkFactory : ILinkFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorisationManager _authorisationManager;
        private readonly IPagingLinkBuilder _pagingLinkBuilder;
        private readonly ISortingLinkBuilder _sortingLinkBuilder;
        private readonly IRouteProvider<PersonaFlags> _routeProvider;

        public PersonaLinkFactory(
            IRouteProvider<PersonaFlags> routeProvider,
            IHttpContextAccessor httpContextAccessor,
            IAuthorisationManager authorisationManager,
            IPagingLinkBuilder pagingLinkBuilder,
            ISortingLinkBuilder sortingLinkBuilder)
        {
            _routeProvider = routeProvider;
            _httpContextAccessor = httpContextAccessor;
            _authorisationManager = authorisationManager;
            _pagingLinkBuilder = pagingLinkBuilder;
            _sortingLinkBuilder = sortingLinkBuilder;
        }

        public ILink Create(
            string linkName,
            string routeName,
            IEnumerable<AuthorisationLink> verbAuthorisationContextTypePairs,
            IResource resource,
            ITemplate template)
        {

            HttpVerb allowVerbs = verbAuthorisationContextTypePairs
                .Where(linkAttribute =>
                {
                    if (linkAttribute.AuthorisationContextType == null)
                    {
                        return true;
                    }

                    if (resource.GetType().IsInstanceOfType(typeof(IEntityResource)))
                    {
                        throw new ArgumentException(
                            "A link specified with an authorisation context must be an instance of IEntityResource");
                    }

                    IAuthorisationContext authorisationContext =
                        (IAuthorisationContext)Activator.CreateInstance(linkAttribute.AuthorisationContextType,
                            resource);
                    return _authorisationManager.HasAccess(authorisationContext);
                })
                .Select(linkAttribute => linkAttribute.HttpVerb)
                .Aggregate<HttpVerb, HttpVerb>(0, (current, f) => current | f);
            string uri = _routeProvider.GetRouteTemplate(routeName,
                _httpContextAccessor.CurrentPersonaFlags(), resource);

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
                Uri = uri
            };
        }
    }
}
