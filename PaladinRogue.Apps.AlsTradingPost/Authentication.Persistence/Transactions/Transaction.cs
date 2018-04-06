using Common.Resources.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Authentication.Persistence.Transactions
{
    public class Transaction : ITransaction
    {
	    private readonly IDbContextTransaction _dbContextTransaction;

	    private Transaction(IDbContextTransaction dbContextTransaction)
	    {
		    _dbContextTransaction = dbContextTransaction;
	    }

	    public static ITransaction Create(IDbContextTransaction dbContextTransaction)
	    {
		    return new Transaction(dbContextTransaction);
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
