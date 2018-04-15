using Common.Resources.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.EntityFramework.Transactions
{
    public class TransactionFactory : ITransactionFactory
    {
	    private readonly DbContext _dbContext;

	    public TransactionFactory(DbContext dbContext)
	    {
		    _dbContext = dbContext;
	    }

	    public ITransaction Create()
	    {
	        IDbContextTransaction dbContextTransaction = _dbContext.Database.BeginTransaction();

	        return Transaction.Create(dbContextTransaction);
        }
    }
}
