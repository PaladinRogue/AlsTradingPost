using AlsTradingPost.Domain.ItemReferenceDataDomain.Models;
using Common.Domain.Services;

namespace AlsTradingPost.Domain.ItemReferenceDataDomain.Interfaces
{
    public interface IItemReferenceDataQueryService : ISummaryQueryService<ItemReferenceDataSummaryProjection>
    {
    }
}
