using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ApplicationServices.Transactions;
using Common.Domain.Persistence;
using Gateway.Domain.Applications;
using Microsoft.EntityFrameworkCore;

namespace Gateway.ApplicationServices.Applications
{
    public class ApplicationApplicationService : IApplicationApplicationService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly IQueryRepository<Application> _queryRepository;

        public ApplicationApplicationService(
            IQueryRepository<Application> queryRepository,
            ITransactionManager transactionManager)
        {
            _queryRepository = queryRepository;
            _transactionManager = transactionManager;
        }

        public async Task<IEnumerable<ApplicationAdto>> GetAllAsync()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                IEnumerable<ApplicationAdto> applicationAdtos = await (await _queryRepository.GetAsync()).Select(a => new ApplicationAdto
                {
                    Name = a.Name,
                    SystemName = a.SystemName,
                    HostUri = a.HostUri
                }).ToListAsync();

                transaction.Commit();

                return applicationAdtos;
            }
        }
    }
}