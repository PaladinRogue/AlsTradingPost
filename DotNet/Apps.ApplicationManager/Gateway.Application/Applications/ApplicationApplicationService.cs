using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Application.Transactions;
using Common.Domain.Persistence;

namespace Gateway.Application.Applications
{
    public class ApplicationApplicationService : IApplicationApplicationService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly IQueryRepository<Domain.Applications.Application> _queryRepository;

        public ApplicationApplicationService(
            IQueryRepository<Domain.Applications.Application> queryRepository,
            ITransactionManager transactionManager)
        {
            _queryRepository = queryRepository;
            _transactionManager = transactionManager;
        }

        public async Task<IEnumerable<ApplicationAdto>> GetAllAsync()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                IEnumerable<ApplicationAdto> applicationAdtos = (await _queryRepository.GetAsync()).Select(a => new ApplicationAdto
                {
                    Name = a.Name,
                    SystemName = a.SystemName,
                    HostUri = a.HostUri
                }).ToList();

                transaction.Commit();

                return applicationAdtos;
            }
        }
    }
}