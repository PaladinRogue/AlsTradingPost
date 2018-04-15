using Common.Api.Pagination;

namespace AlsTradingPost.Api.ItemReferenceData
{
    public class ItemReferenceDataSearchTemplate : PaginationTemplate
    {
        public ItemReferenceDataSearchTemplate()
        {
            PageSize = 10;
        }

        public string Name { get; set; }
    }
}
