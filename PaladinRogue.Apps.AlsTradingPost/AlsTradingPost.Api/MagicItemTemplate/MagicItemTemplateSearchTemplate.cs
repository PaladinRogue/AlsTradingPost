using System.Collections.Generic;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Pagination.Interfaces;
using Common.Api.Sorting;
using Common.Api.Validation.Attributes;
using Common.Resources.Sorting;
using Common.Setup.Infrastructure.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AlsTradingPost.Api.MagicItemTemplate
{
    [SelfLink(RouteDictionary.MagicItemTemplateSearchTemplate, HttpVerbs.Get)]
    [SearchLink(RouteDictionary.MagicItemTemplateGet)]
    public class MagicItemTemplateSearchTemplate : IPaginationTemplate, ISortTemplate
    {
        public MagicItemTemplateSearchTemplate()
        {
            PageOffset = 0;
            PageSize = 10;
        }
        
        [Length(3, 50)]
        public string Name { get; set; }
        [FromQuery(Name = "page[offset]")]
        public int PageOffset { get; set; }
        public int PageSize { get; set; }
        public IList<SortBy> Sort { get; set; }
    }
}
