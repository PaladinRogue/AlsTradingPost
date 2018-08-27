using Authentication.Domain.Models;
using Common.Domain.Persistence;

namespace Authentication.Domain.Persistence
{
    public interface IIdentityRepository : IGetById<Identity>, IGetSingle<Identity>, IAdd<Identity>, IUpdate<Identity>
    {
    }
}
