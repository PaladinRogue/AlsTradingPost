using Authentication.Domain.Models;
using Common.Persistence.Interfaces;

namespace Authentication.Persistence.Interfaces
{
    public interface IIdentityRepository : IRepository<Identity>
    {
    }
}
