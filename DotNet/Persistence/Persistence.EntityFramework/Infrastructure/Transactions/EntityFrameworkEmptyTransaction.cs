using Common.Application.Transactions;

namespace Persistence.EntityFramework.Infrastructure.Transactions
{
    public class EntityFrameworkEmptyTransaction : ITransaction
    {
        private EntityFrameworkEmptyTransaction()
        {
        }

        public static ITransaction Create()
        {
            return new EntityFrameworkEmptyTransaction();
        }

        public void Dispose()
        {
        }

        public void Commit()
        {
        }

        public void Rollback()
        {
        }
    }
}
