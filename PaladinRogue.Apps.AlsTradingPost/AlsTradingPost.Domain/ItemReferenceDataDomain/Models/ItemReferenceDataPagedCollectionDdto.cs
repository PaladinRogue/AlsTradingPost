using System.Collections.Generic;
using Common.Domain.Pagination;
using Common.Domain.Pagination.Interfaces;

namespace AlsTradingPost.Domain.ItemReferenceDataDomain.Models
{
    public class ItemReferenceDataPagedCollectionDdto : PagedCollectionDdto<ItemReferenceDataSummaryProjection, ItemReferenceDataPagedCollectionDdto>
    {
        public ItemReferenceDataPagedCollectionDdto(IList<ItemReferenceDataSummaryProjection> results, int totalResults, IPagination paginationDdto)
            : base(results, totalResults, paginationDdto)
        {
        }
    }
}
