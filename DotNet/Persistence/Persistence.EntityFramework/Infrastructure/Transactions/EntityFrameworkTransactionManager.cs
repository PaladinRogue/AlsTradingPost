using Common.ApplicationServices.Transactions;
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
            if (_dbContext.Database.CurrentTransaction != null)
            {
                return EntityFrameworkEmptyTransaction.Create(_dbContext.Database.CurrentTransaction);
            }

            return EntityFrameworkTransaction.Create(_dbContext, _dbContext.Database.BeginTransaction());
        }
    }
}
