using Vault.Persistence;
using Persistence.EntityFramework.Infrastructure.Transactions;

namespace Vault.Broker.Persistence
{
    public class VaultTransactionManager : EntityFrameworkTransactionManager<VaultDbContext>, IVaultTransactionManager
    {
        public VaultTransactionManager(VaultDbContext dbContext) : base(dbContext)
        {
        }
    }
}