using System.Threading.Tasks;
using ApplicationManager.Domain.Applications;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Persistence;

namespace ReverseProxy.ApplicationServices.Applications
{
    public class ApplicationKernalService : IApplicationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly IApplicationCache _applicationCache;

        private readonly IQueryRepository<Application> _queryRepository;

        public ApplicationKernalService(
            IApplicationCache applicationCache,
            IQueryRepository<Application> queryRepository,
            ITransactionManager transactionManager)
        {
            _applicationCache = applicationCache;
            _queryRepository = queryRepository;
            _transactionManager = transactionManager;
        }

        public async Task<ApplicationAdto> GetByNameAsync(string applicationName)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Application application = await _applicationCache.GetAsync(applicationName);

                if (application == null)
                {
                    application = await _queryRepository.GetSingleAsync(a => a.SystemName == applicationName);

                    if (application == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.NotFound, $"Application: {applicationName} not recognised");
                    }

                    await _applicationCache.AddAsync(application);
                }

                transaction.Commit();

                return new ApplicationAdto
                {
                    ApplicationName = application.SystemName,
                    HostUri = application.HostUri
                };
            }
        }

        public async Task CreateAsync(Application application)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                await _applicationCache.AddAsync(application);

                transaction.Commit();
            }
        }

        public async Task UpdateAsync(Application application)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                await _applicationCache.UpdateAsync(application);

                transaction.Commit();
            }
        }
    }
}