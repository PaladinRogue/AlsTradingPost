using Authentication.Domain.Models;
using Common.Persistence.Interfaces;

namespace Authentication.Persistence.Interfaces
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Application GetByName(string applicationName);
    }
}
