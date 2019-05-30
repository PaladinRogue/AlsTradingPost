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
            IDbContextTransaction dbContextTransaction = _dbContext.Database.CurrentTransaction ?? _dbContext.Database.BeginTransaction();

            if (_dbContext.Database.CurrentTransaction != null)
            {
                return EntityFrameworkEmptyTransaction.Create(dbContextTransaction);
            }

            return EntityFrameworkTransaction.Create(_dbContext, dbContextTransaction);
        }
    }
}
