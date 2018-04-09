using Common.Resources.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Authentication.Persistence.Transactions
{
    public class TransactionFactory : ITransactionFactory
    {
	    private readonly AuthenticationDbContext _dbContext;

	    public TransactionFactory(AuthenticationDbContext dbContext)
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
