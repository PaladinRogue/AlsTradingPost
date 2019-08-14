using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaladinRogue.Library.Vault.Domain.DataKeys.Persistence
{
    public interface IDataKeyRepository
    {
        Task<IEnumerable<Core.Domain.DataProtectors.DataKey>> GetAllSharedAsync();

        Task<Core.Domain.DataProtectors.DataKey> GetSharedAsync(string name);

        Task<IEnumerable<Core.Domain.DataProtectors.DataKey>> GetAllAsync();

        Task<Core.Domain.DataProtectors.DataKey> GetAsync(string name);

        Task CreateKeyAsync(Core.Domain.DataProtectors.DataKey dataKey);
    }
}