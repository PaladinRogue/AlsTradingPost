using System.Threading.Tasks;
using Persistence.EntityFramework.Repositories;
using Gateway.Domain.Applications;
using Gateway.Domain.Applications.Persistence;

namespace Gateway.Persistence.Applications
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