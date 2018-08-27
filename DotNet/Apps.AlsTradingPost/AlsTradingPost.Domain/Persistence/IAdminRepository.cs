using AlsTradingPost.Domain.Models;
using Common.Domain.Persistence;

namespace AlsTradingPost.Domain.Persistence
{
    public interface IAdminRepository : IGetById<Admin>, IAdd<Admin>, IUpdate<Admin>
    {
    }
}
