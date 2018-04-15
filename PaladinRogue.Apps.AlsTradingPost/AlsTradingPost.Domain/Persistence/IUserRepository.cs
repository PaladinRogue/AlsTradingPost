using AlsTradingPost.Domain.Models;
using Common.Domain.Persistence;

namespace AlsTradingPost.Domain.Persistence
{
    public interface IUserRepository : IGet<User>, IGetPage<User>, IGetById<User>, IGetSingle<User>, IAdd<User>, IUpdate<User>, IDelete
    {
    }
}
