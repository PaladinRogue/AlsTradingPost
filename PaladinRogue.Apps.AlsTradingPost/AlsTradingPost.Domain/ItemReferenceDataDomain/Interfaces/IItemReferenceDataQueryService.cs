using AlsTradingPost.Domain.ItemReferenceDataDomain.Models;
using AlsTradingPost.Domain.Models;
using Common.Domain.Services.Interfaces;

namespace AlsTradingPost.Domain.ItemReferenceDataDomain.Interfaces
{
    public interface IItemReferenceDataQueryService : IPagedSummaryQueryService<ItemReferenceData, ItemReferenceDataPagedCollectionDdto>
    {
    }
}
