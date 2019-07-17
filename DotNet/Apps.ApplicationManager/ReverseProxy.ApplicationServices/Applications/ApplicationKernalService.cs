using System.Threading.Tasks;
using Common.ApplicationServices.Caching;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using ReverseProxy.Domain.Applications;
using ReverseProxy.Domain.Applications.Persistence;

namespace ReverseProxy.ApplicationServices.Applications
{
    public class ApplicationKernalService : IApplicationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly IApplicationQueryRepository _queryRepository;

        private readonly ICacheDecorator<string, Application> _cacheDecorator;

        public ApplicationKernalService(
            IApplicationQueryRepository queryRepository,
            ITransactionManager transactionManager,
            ICacheDecorator<string, Application> cacheDecorator)
        {
            _queryRepository = queryRepository;
            _transactionManager = transactionManager;
            _cacheDecorator = cacheDecorator;
        }

        public async Task<ApplicationAdto> GetByNameAsync(string applicationName)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Application application = await _queryRepository.GetByNameAsync(applicationName);

                if (application == null)
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, $"Application: {applicationName} not recognised");
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
                await _cacheDecorator.AddAsync(application.Name, application);

                transaction.Commit();
            }
        }

        public async Task UpdateAsync(Application application)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                await _cacheDecorator.UpdateAsync(application.Name,  application);

                transaction.Commit();
            }
        }
    }
}