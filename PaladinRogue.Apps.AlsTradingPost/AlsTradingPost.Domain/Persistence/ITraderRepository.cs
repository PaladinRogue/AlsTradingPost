using AlsTradingPost.Domain.Models;
using Common.Domain.Persistence;

namespace AlsTradingPost.Domain.Persistence
{
    public interface ITraderRepository : IGetById<Trader>, IAdd<Trader>, IUpdate<Trader>, ICheckConcurrency
    {
    }
}
