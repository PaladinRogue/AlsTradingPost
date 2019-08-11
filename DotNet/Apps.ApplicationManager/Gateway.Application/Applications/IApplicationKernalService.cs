using System.Threading.Tasks;

namespace PaladinRogue.Gateway.Application.Applications
{
    public interface IApplicationKernalService
    {
        Task<ApplicationAdto> GetByNameAsync(string applicationName);

        Task CreateAsync(Domain.Applications.Application application);

        Task UpdateAsync(Domain.Applications.Application application);
    }
}