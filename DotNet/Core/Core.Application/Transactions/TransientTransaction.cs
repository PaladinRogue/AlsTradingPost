using System.Transactions;

namespace PaladinRogue.Libray.Core.Application.Transactions
{
    public class TransientTransaction : ITransaction
    {
	    private readonly TransactionScope _transactionScope;

	    private TransientTransaction(TransactionScope transactionScope)
	    {
            _transactionScope = transactionScope;
	    }

	    public static ITransaction Create(TransactionScope transactionScope)
	    {
		    return new TransientTransaction(transactionScope);
	    }

	    public void Dispose()
	    {
	        _transactionScope.Dispose();
		}

	    public void Commit()
		{
		    _transactionScope.Complete();
		}

	    public void Rollback()
		{
		    _transactionScope.Dispose();
		}
    }
}
