using System;
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

                _applicationCache.Add(application);
            }

            return new ApplicationAdto
            {
                ApplicationName = application.SystemName,
                //TODO
                HostUri = new Uri("")
            };
        }
    }

    public interface IApplicationCache
    {
        Task<Application> GetAsync(string name);

        Task Add(Application application);

        Task Remove(string name);

        Task Update(string name);
    }

    public interface IApplicationKernalService
    {
        Task<ApplicationAdto> GetByNameAsync(string applicationName);
    }

    public class ApplicationAdto
    {
        public string ApplicationName { get; set; }

        public Uri HostUri { get; set; }
    }
}