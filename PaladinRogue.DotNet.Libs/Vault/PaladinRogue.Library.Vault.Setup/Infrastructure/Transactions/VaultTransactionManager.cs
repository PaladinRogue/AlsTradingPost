using PaladinRogue.Library.Persistence.Setup.Infrastructure.Transactions;
using PaladinRogue.Library.Vault.Persistence;

namespace PaladinRogue.Library.Vault.Setup.Infrastructure.Transactions
{
    public class VaultTransactionManager : EntityFrameworkTransactionManager<VaultDbContext>, IVaultTransactionManager
    {
        public VaultTransactionManager(VaultDbContext dbContext) : base(dbContext)
        {
        }
    }
}