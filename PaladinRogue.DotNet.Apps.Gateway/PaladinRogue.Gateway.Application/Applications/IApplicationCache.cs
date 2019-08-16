using System.Threading.Tasks;

namespace PaladinRogue.Gateway.Application.Applications
{
    public interface IApplicationCache
    {
        Task<Domain.Applications.Application> GetAsync(string name);

        Task AddAsync(Domain.Applications.Application application);

        Task RemoveAsync(string name);

        Task UpdateAsync(Domain.Applications.Application application);
    }
}