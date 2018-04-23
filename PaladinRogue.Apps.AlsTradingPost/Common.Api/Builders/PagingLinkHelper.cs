using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Constants;
using Common.Api.Links;
using Common.Api.Pagination.Interfaces;
using Common.Resources.Extensions;

namespace Common.Api.Builders
{
    public static class PagingLinkHelper
    {
        private static readonly string[] GetVerb = { HttpVerbs.Get };

        public static IEnumerable<Link> GetPagingLinks(IPaginationTemplate paginationTemplate, int totalResults)
        {
            List<Link> links = new List<Link>();

            if (paginationTemplate.PageOffset != 0)
            {
                links.Add(PageLink(LinkType.FirstPage, paginationTemplate, 0));
                links.Add(PageLink(LinkType.PreviousPage, paginationTemplate, Math.Max(0, paginationTemplate.PageOffset - paginationTemplate.PageSize)));
            }

            if (paginationTemplate.PageOffset + paginationTemplate.PageSize < totalResults)
            {
                links.Add(PageLink(LinkType.NextPage, paginationTemplate, paginationTemplate.PageOffset + paginationTemplate.PageSize));
                links.Add(PageLink(LinkType.LastPage, paginationTemplate, totalResults - paginationTemplate.PageSize));
            }

            return links;
        }

        private static Link PageLink(string linkType, IPaginationTemplate paginationTemplate, int pageOffset)
        {
            return new Link
            {
                Name = linkType,
                AllowVerbs = GetVerb,
                QueryParams = GetPagingDictionary(paginationTemplate, pageOffset)
            };
        }

        private static IDictionary<string, string> GetPagingDictionary(IPaginationTemplate paginationTemplate, int pageOffset)
        {
            return paginationTemplate.GetType().GetProperties()
                .Where(p => p.GetValue(paginationTemplate) != null || p.Name == nameof(paginationTemplate.PageOffset))
                .ToDictionary(
                    p => p.Name.ToCamelCase(), 
                    p => p.Name == nameof(paginationTemplate.PageOffset) ? pageOffset.ToString() : p.GetValue(paginationTemplate).ToString().ToCamelCase()
                );
        }
    }
}