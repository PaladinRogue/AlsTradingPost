using Common.Application.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.EntityFramework.Infrastructure.Transactions
{
    public class EntityFrameworkTransaction : ITransaction
    {
        private readonly IDbContextTransaction _dbContextTransaction;

        private EntityFrameworkTransaction(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction;
        }

        public static ITransaction Create(IDbContextTransaction dbContextTransaction)
        {
            return new EntityFrameworkTransaction(dbContextTransaction);
        }

        public void Dispose()
        {
            _dbContextTransaction.Dispose();
        }

        public void Commit()
        {
            _dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }
    }
}
