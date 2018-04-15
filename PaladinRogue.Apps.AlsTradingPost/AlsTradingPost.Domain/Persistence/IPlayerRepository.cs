using AlsTradingPost.Domain.Models;
using Common.Domain.Persistence;

namespace AlsTradingPost.Domain.Persistence
{
    public interface IPlayerRepository : IGet<Player>, IGetPage<Player>, IGetById<Player>, IGetSingle<Player>, IAdd<Player>, IUpdate<Player>, IDelete
    {
    }
}
