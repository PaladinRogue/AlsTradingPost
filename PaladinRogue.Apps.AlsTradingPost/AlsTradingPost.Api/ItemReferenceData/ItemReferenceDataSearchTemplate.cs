using Common.Api.Pagination;
using Common.Api.Sorting;
using Common.Resources.Extensions;

namespace AlsTradingPost.Api.ItemReferenceData
{
    public class ItemReferenceDataSearchTemplate : IPaginationTemplate, IThenByTemplate
    {
        public ItemReferenceDataSearchTemplate()
        {
            PageOffset = 0;
            PageSize = 10;
            OrderBy = nameof(Name).ToCamelCase();
            OrderByAscending = true;
        }

        public string Name { get; set; }
        public int PageOffset { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByAscending { get; set; }
        public string ThenBy { get; set; }
        public bool ThenByAscending { get; set; }
    }
}
