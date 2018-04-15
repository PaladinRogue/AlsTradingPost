using Authentication.Domain.Models;
using Common.Domain.Persistence;

namespace Authentication.Domain.Persistence
{
    public interface IApplicationRepository : IGetById<Application>, IGetSingle<Application>, IUpdate<Application>, IAdd<Application>
    {
    }
}
