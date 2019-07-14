using System.Threading.Tasks;
using ReverseProxy.Domain.Applications;

namespace ReverseProxy.ApplicationServices.Applications
{
    public interface IApplicationCache
    {
        Task<Application> GetAsync(string name);

        Task AddAsync(Application application);

        Task RemoveAsync(string name);

        Task UpdateAsync(Application application);
    }
}