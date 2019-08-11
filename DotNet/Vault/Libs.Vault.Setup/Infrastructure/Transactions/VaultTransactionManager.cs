using PaladinRogue.Libray.Persistence.Setup.Infrastructure.Transactions;
using PaladinRogue.Libray.Vault.Persistence;

namespace PaladinRogue.Libray.Vault.Setup.Infrastructure.Transactions
{
    public class VaultTransactionManager : EntityFrameworkTransactionManager<VaultDbContext>, IVaultTransactionManager
    {
        public VaultTransactionManager(VaultDbContext dbContext) : base(dbContext)
        {
        }
    }
}