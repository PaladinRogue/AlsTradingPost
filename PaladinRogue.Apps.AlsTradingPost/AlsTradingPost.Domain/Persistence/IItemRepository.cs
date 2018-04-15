using AlsTradingPost.Domain.Models;
using Common.Domain.Persistence;

namespace AlsTradingPost.Domain.Persistence
{
    public interface IItemRepository : IGet<Item>, IGetPage<Item>, IGetById<Item>, IGetSingle<Item>, IAdd<Item>, IUpdate<Item>, IDelete
    {
    }
}
