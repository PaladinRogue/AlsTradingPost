using System.Threading.Tasks;
using Persistence.EntityFramework.Repositories;
using ReverseProxy.Domain.Applications;
using ReverseProxy.Domain.Applications.Persistence;

namespace ReverseProxy.Persistence.Applications
{
    public class ApplicationQueryRepository : IApplicationQueryRepository
    {
        private readonly ReverseProxyDbContext _reverseProxyDbContext;

        public ApplicationQueryRepository(ReverseProxyDbContext reverseProxyDbContext)
        {
            _reverseProxyDbContext = reverseProxyDbContext;
        }

        public Task<Application> GetByNameAsync(string name)
        {
            return RepositoryHelper.GetSingleAsync(_reverseProxyDbContext.Applications, a => a.SystemName == name);
        }
    }
}