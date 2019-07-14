using System.Threading.Tasks;
using ReverseProxy.Domain.Applications;

namespace ReverseProxy.ApplicationServices.Applications
{
    public interface IApplicationKernalService
    {
        Task<ApplicationAdto> GetByNameAsync(string applicationName);

        Task CreateAsync(Application application);

        Task UpdateAsync(Application application);
    }
}