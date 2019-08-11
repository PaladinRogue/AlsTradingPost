using System.Threading.Tasks;
using PaladinRogue.Gateway.Domain.Applications;
using PaladinRogue.Gateway.Domain.Applications.Persistence;
using PaladinRogue.Libray.Persistence.EntityFramework.Repositories;

namespace PaladinRogue.Gateway.Persistence.Applications
{
    public class ApplicationQueryRepository : IApplicationQueryRepository
    {
        private readonly GatewayDbContext _gatewayDbContext;

        public ApplicationQueryRepository(GatewayDbContext gatewayDbContext)
        {
            _gatewayDbContext = gatewayDbContext;
        }

        public Task<Application> GetByNameAsync(string name)
        {
            return RepositoryHelper.GetSingleAsync(_gatewayDbContext.Applications, a => a.SystemName == name);
        }
    }
}