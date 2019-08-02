using System.Threading.Tasks;
using Gateway.Domain.Applications;

namespace Gateway.ApplicationServices.Applications
{
    public interface IApplicationCache
    {
        Task<Application> GetAsync(string name);

        Task AddAsync(Application application);

        Task RemoveAsync(string name);

        Task UpdateAsync(Application application);
    }
}