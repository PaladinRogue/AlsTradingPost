using Common.Application.Pagination;

namespace AlsTradingPost.Application.ItemReferenceDataApplication.Models
{
    public class ItemReferenceDataSearchAdto : PaginationAdto
    {
        public string Name { get; set; }
    }
}
