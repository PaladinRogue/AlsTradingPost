using System.Collections.Generic;
using Common.Domain.Pagination;

namespace AlsTradingPost.Domain.ItemReferenceDataDomain.Models
{
    public class ItemReferenceDataPagedCollectionDdto : PagedCollectionDdto<ItemReferenceDataSummaryProjection, ItemReferenceDataPagedCollectionDdto>
    {
        public ItemReferenceDataPagedCollectionDdto(IList<ItemReferenceDataSummaryProjection> results, int totalResults)
            : base(results, totalResults)
        {
        }
    }
}
