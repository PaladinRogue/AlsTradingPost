using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PaladinRogue.Libray.Core.Api.Pagination.Interfaces;
using PaladinRogue.Libray.Core.Common.Builders.Dictionaries;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Libray.Core.Api.Links
{
    public class DefaultPagingLinkBuilder : IPagingLinkBuilder
    {
        public PageLink BuildLink(string name, string uri, IPaginationTemplate paginationTemplate)
        {
            return _pageLink(name, uri, paginationTemplate, paginationTemplate.PageOffset);
        }

        public PagingLinks BuildPagingLinks(IPaginationTemplate paginationTemplate, ILink selfLink, int? totalResults)
        {
            if (!(selfLink is PageLink pageLink))
            {
                return null;
            }

            if (paginationTemplate == null || !totalResults.HasValue)
            {
                return null;
            }

            pageLink.Sort = paginationTemplate.Sort;
            pageLink.PageOffset = paginationTemplate.PageOffset;
            pageLink.PageSize = paginationTemplate.PageSize;
            pageLink.QueryParams = _getAdditionalParams(paginationTemplate);

            PagingLinks links = new PagingLinks();

            if (paginationTemplate.PageOffset != 0)
            {
                links.First = _pageLink(LinkType.FirstPage, pageLink.Uri, paginationTemplate, 0);
                links.Previous = _pageLink(LinkType.PreviousPage, pageLink.Uri, paginationTemplate, Math.Max(0, paginationTemplate.PageOffset - paginationTemplate.PageSize));
            }

            if (paginationTemplate.PageOffset + paginationTemplate.PageSize < totalResults)
            {
                links.Next =_pageLink(LinkType.NextPage, pageLink.Uri,  paginationTemplate, paginationTemplate.PageOffset + paginationTemplate.PageSize);
                links.Last = _pageLink(LinkType.LastPage, pageLink.Uri, paginationTemplate, totalResults.Value - paginationTemplate.PageSize);
            }

            return links;
        }

        private PageLink _pageLink(string linkType, string pageLinkUri, IPaginationTemplate paginationTemplate, int pageOffset)
        {
            return new PageLink
            {
                Uri = pageLinkUri,
                Name = linkType,
                AllowVerbs = HttpVerb.Get,
                Sort = paginationTemplate.Sort,
                PageOffset = pageOffset,
                PageSize = paginationTemplate.PageSize,
                QueryParams = _getAdditionalParams(paginationTemplate)
            };
        }

        private IDictionary<string, object> _getAdditionalParams(IPaginationTemplate paginationTemplate)
        {
            DictionaryBuilder<string, object> dictionaryBuilder = DictionaryBuilder<string, object>.Create();

            List<string> handledProperties = new List<string>
            {
                nameof(paginationTemplate.PageSize),
                nameof(paginationTemplate.PageOffset),
                nameof(paginationTemplate.Sort)
            };

            foreach (PropertyInfo propertyInfo in paginationTemplate.GetType().GetProperties().Where(p => !handledProperties.Contains(p.Name)))
            {
                dictionaryBuilder.Add(propertyInfo.Name, propertyInfo.GetValue(paginationTemplate));
            }

            return dictionaryBuilder.Build();
        }
    }
}