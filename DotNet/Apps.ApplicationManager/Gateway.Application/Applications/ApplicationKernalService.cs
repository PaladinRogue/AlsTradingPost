using System.Threading.Tasks;
using PaladinRogue.Gateway.Domain.Applications.Persistence;
using PaladinRogue.Libray.Core.Application.Caching;
using PaladinRogue.Libray.Core.Application.Exceptions;
using PaladinRogue.Libray.Core.Application.Transactions;

namespace PaladinRogue.Gateway.Application.Applications
{
    public class ApplicationKernalService : IApplicationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly IApplicationQueryRepository _queryRepository;

        private readonly ICacheDecorator<string, Domain.Applications.Application> _cacheDecorator;

        public ApplicationKernalService(
            IApplicationQueryRepository queryRepository,
            ITransactionManager transactionManager,
            ICacheDecorator<string, Domain.Applications.Application> cacheDecorator)
        {
            _queryRepository = queryRepository;
            _transactionManager = transactionManager;
            _cacheDecorator = cacheDecorator;
        }

        public async Task<ApplicationAdto> GetByNameAsync(string applicationName)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Domain.Applications.Application application = await _queryRepository.GetByNameAsync(applicationName);

                if (application == null)
                {
                    throw new BusinessApplicationException(ExceptionType.NotFound, $"Application: {applicationName} not recognised");
                }

                transaction.Commit();

                return new ApplicationAdto
                {
                    Name = application.Name,
                    SystemName = application.SystemName,
                    HostUri = application.HostUri
                };
            }
        }

        public async Task CreateAsync(Domain.Applications.Application application)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                await _cacheDecorator.AddAsync(application.Name, application);

                transaction.Commit();
            }
        }

        public async Task UpdateAsync(Domain.Applications.Application application)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                await _cacheDecorator.UpdateAsync(application.Name,  application);

                transaction.Commit();
            }
        }
    }
}