using System.Threading.Tasks;
using ApplicationManager.Domain.Applications;
using Common.Domain.Persistence;

namespace ReverseProxy.ApplicationServices.Applications
{
    public class ApplicationKernalService : IApplicationKernalService
    {
        private readonly IApplicationCache _applicationCache;

        private readonly IQueryRepository<Application> _queryRepository;

        public ApplicationKernalService(
            IApplicationCache applicationCache,
            IQueryRepository<Application> queryRepository)
        {
            _applicationCache = applicationCache;
            _queryRepository = queryRepository;
        }

        public async Task<ApplicationAdto> GetByNameAsync(string applicationName)
        {
            Application application = await _applicationCache.GetAsync(applicationName);

            if (application == null)
            {
                application = await _queryRepository.GetSingleAsync(a => a.SystemName == applicationName);

                await _applicationCache.AddAsync(application);
            }

            return new ApplicationAdto
            {
                ApplicationName = application.SystemName,
                HostUri = application.HostUri
            };
        }
    }
}