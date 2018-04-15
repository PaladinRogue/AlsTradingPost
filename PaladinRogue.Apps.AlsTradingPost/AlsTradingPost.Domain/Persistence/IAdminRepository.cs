using AlsTradingPost.Domain.Models;
using Common.Domain.Persistence;

namespace AlsTradingPost.Domain.Persistence
{
    public interface IAdminRepository : IGet<Admin>, IGetPage<Admin>, IGetById<Admin>, IGetSingle<Admin>, IAdd<Admin>, IUpdate<Admin>, IDelete
    {
    }
}
