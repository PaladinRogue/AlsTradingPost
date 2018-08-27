using Common.Application.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.EntityFramework.Infrastructure.Transactions
{
    public class EntityFrameworkTransactionManager : ITransactionManager
    {
        private readonly DbContext _dbContext;

        public EntityFrameworkTransactionManager(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ITransaction Create()
        {
            IDbContextTransaction dbContextTransaction = _dbContext.Database.BeginTransaction();

            return EntityFrameworkTransaction.Create(dbContextTransaction);
        }
    }
}
