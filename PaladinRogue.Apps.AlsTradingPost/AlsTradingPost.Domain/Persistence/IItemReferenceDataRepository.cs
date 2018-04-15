using AlsTradingPost.Domain.Models;
using Common.Domain.Persistence;

namespace AlsTradingPost.Domain.Persistence
{
    public interface IItemReferenceDataRepository : IGet<ItemReferenceData>, IGetPage<ItemReferenceData>, IGetById<ItemReferenceData>,
        IGetSingle<ItemReferenceData>, IAdd<ItemReferenceData>, IUpdate<ItemReferenceData>, IDelete
    {
    }
}
