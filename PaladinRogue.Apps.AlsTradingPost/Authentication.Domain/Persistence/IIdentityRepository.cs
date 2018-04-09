using Authentication.Domain.Models;
using Common.Domain.Persistence;

namespace Authentication.Domain.Persistence
{
    public interface IIdentityRepository : IRepository<Identity>
    {
    }
}
