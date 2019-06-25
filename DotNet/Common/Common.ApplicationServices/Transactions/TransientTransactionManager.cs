using System.Transactions;

namespace Common.ApplicationServices.Transactions
{
    public class TransientTransactionManager : ITransactionManager
    {
        public ITransaction Create()
        {
            return TransientTransaction.Create(new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadCommitted
                    },
                    TransactionScopeAsyncFlowOption.Enabled
                )
            );
        }
    }
}
