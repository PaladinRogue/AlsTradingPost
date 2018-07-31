using System.Collections.Generic;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;
using Common.Api.Sorting;
using Common.Api.Validation.Attributes;
using Common.Resources.Sorting;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.MagicItemTemplate
{
    [SelfLink(RouteDictionary.MagicItemTemplateSearchTemplate, HttpVerbs.Get)]
    [SearchLink(RouteDictionary.MagicItemTemplateGet)]
    public class MagicItemTemplateSearchTemplate : ISearchTemplate, IPaginationTemplate, ISortTemplate
    {
        public MagicItemTemplateSearchTemplate()
        {
            PageOffset = 0;
            PageSize = 10;
        }
        
        [Length(3, 50)]
        public string Name { get; set; }
        public int PageOffset { get; set; }
        public int PageSize { get; set; }
        public IList<SortBy> Sort { get; set; }
    }
}
