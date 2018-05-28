using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Pagination.Interfaces;
using Common.Api.Sorting;
using Common.Api.Validation.Attributes;
using Common.Resources.Extensions;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.MagicItemTemplate
{
    [SelfLink(RouteDictionary.MagicItemTemplateSearchTemplate, HttpVerbs.Get)]
    [SearchLink(RouteDictionary.MagicItemTemplateGet)]
    public class MagicItemTemplateSearchTemplate : IPaginationTemplate, IThenByTemplate
    {
        public MagicItemTemplateSearchTemplate()
        {
            PageOffset = 0;
            PageSize = 10;
            OrderBy = nameof(Name).ToCamelCase();
            OrderByAscending = true;
        }
        
        [Length(3, 50)]
        public string Name { get; set; }
        public int PageOffset { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByAscending { get; set; }
        public string ThenBy { get; set; }
        public bool? ThenByAscending { get; set; }
    }
}
