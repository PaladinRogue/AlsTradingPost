using System.Threading.Tasks;

namespace ReverseProxy.Domain.Applications.Persistence
{
    public interface IApplicationQueryRepository
    {
        Task<Application> GetByNameAsync(string name);
    }
}