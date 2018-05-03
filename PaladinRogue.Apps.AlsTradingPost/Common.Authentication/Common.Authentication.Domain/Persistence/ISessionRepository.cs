using Common.Authentication.Domain.Models;
using Common.Domain.Persistence;

namespace Common.Authentication.Domain.Persistence
{
    public interface ISessionRepository : IGetById<Session>, IAdd<Session>, IUpdate<Session>
    {
    }
}
