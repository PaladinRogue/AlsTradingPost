using KeyVault.Persistence;
using Persistence.EntityFramework.Infrastructure.Transactions;

namespace KeyVault.Broker.Persistence
{
    public class KeyVaultTransactionManager : EntityFrameworkTransactionManager<KeyVaultDbContext>, IKeyVaultTransactionManager
    {
        public KeyVaultTransactionManager(KeyVaultDbContext dbContext) : base(dbContext)
        {
        }
    }
}