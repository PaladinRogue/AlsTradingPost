using Authentication.Domain.Models;
using Common.Domain.Persistence;

namespace Authentication.Domain.Persistence
{
    public interface IIdentityRepository : IRepository<Identity>
    {
        Identity GetByAuthenticationId(string authenticationId);
    }
}
