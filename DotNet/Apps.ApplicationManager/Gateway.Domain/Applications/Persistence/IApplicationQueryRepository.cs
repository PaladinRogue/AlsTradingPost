using System.Threading.Tasks;

namespace PaladinRogue.Gateway.Domain.Applications.Persistence
{
    public interface IApplicationQueryRepository
    {
        Task<Application> GetByNameAsync(string name);
    }
}