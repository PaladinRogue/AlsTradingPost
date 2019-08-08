using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Domain.DataProtectors;

namespace Libs.Vault.Domain.Domain.Persistence
{
    public interface IDataKeyRepository
    {
        Task<IEnumerable<DataKey>> GetAllSharedAsync();

        Task<DataKey> GetSharedAsync(string name);

        Task<IEnumerable<DataKey>> GetAllAsync();

        Task<DataKey> GetAsync(string name);

        Task CreateKeyAsync(DataKey dataKey);
    }
}