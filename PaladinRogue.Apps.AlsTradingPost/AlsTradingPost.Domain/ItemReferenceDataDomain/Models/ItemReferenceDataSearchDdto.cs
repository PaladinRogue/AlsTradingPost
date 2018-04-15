using Common.Domain.Pagination;

namespace AlsTradingPost.Domain.ItemReferenceDataDomain.Models
{
    public class ItemReferenceDataSearchDdto : PaginationDdto
    {
        public string Name { get; set; }
    }
}
