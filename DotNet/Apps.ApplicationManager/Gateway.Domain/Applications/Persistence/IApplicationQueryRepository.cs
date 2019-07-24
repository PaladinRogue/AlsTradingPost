using System.Threading.Tasks;

namespace Gateway.Domain.Applications.Persistence
{
    public interface IApplicationQueryRepository
    {
        Task<Application> GetByNameAsync(string name);
    }
}