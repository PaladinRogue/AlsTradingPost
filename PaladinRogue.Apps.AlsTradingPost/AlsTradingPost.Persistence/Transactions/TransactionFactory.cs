using Common.Resources.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

namespace AlsTradingPost.Persistence.Transactions
{
    public class TransactionFactory : ITransactionFactory
    {
	    private readonly AlsTradingPostDbContext _dbContext;

	    public TransactionFactory(AlsTradingPostDbContext dbContext)
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
