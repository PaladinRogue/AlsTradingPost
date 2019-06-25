using Common.ApplicationServices.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.EntityFramework.Infrastructure.Transactions
{
    public class EntityFrameworkTransaction : ITransaction
    {
        private readonly DbContext _dbContext;

        private readonly IDbContextTransaction _dbContextTransaction;

        private EntityFrameworkTransaction(DbContext dbContext, IDbContextTransaction dbContextTransaction)
        {
            _dbContext = dbContext;
            _dbContextTransaction = dbContextTransaction;
        }

        public static ITransaction Create(DbContext dbContext, IDbContextTransaction dbContextTransaction)
        {
            return new EntityFrameworkTransaction(dbContext, dbContextTransaction);
        }

        public void Dispose()
        {
            _dbContextTransaction.Dispose();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();

            _dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }
    }
}
