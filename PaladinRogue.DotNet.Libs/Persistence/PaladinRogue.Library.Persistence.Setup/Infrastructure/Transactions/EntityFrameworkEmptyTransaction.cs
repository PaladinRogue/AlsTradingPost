using Microsoft.EntityFrameworkCore.Storage;
using PaladinRogue.Library.Core.Application.Transactions;

namespace PaladinRogue.Library.Persistence.Setup.Infrastructure.Transactions
{
    public class EntityFrameworkEmptyTransaction : ITransaction
    {
        private readonly IDbContextTransaction _dbContextTransaction;

        private EntityFrameworkEmptyTransaction(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction;
        }

        public static ITransaction Create(IDbContextTransaction dbContextTransaction)
        {
            return new EntityFrameworkEmptyTransaction(dbContextTransaction);
        }

        public void Dispose()
        {
        }

        public void Commit()
        {
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }
    }
}
