using System.Collections.Generic;
using Common.Domain.Pagination;

namespace AlsTradingPost.Domain.MagicItemTemplateDomain.Models
{
    public class MagicItemTemplatePagedCollectionDdto : PagedCollectionDdto<MagicItemTemplateSummaryProjection, MagicItemTemplatePagedCollectionDdto>
    {
        public MagicItemTemplatePagedCollectionDdto(IList<MagicItemTemplateSummaryProjection> results, int totalResults)
            : base(results, totalResults)
        {
        }
    }
}
