using Authentication.Domain.Models;
using Common.Domain.Persistence;

namespace Authentication.Domain.Persistence
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Application GetByName(string applicationName);
    }
}
