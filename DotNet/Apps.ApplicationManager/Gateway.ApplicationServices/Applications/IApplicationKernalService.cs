using System.Threading.Tasks;
using Gateway.Domain.Applications;

namespace Gateway.ApplicationServices.Applications
{
    public interface IApplicationKernalService
    {
        Task<ApplicationAdto> GetByNameAsync(string applicationName);

        Task CreateAsync(Application application);

        Task UpdateAsync(Application application);
    }
}