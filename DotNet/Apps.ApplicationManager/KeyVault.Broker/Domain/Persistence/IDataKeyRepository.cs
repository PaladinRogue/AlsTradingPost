using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KeyVault.Domain.SharedDataKeys;

namespace KeyVault.Broker.Domain.Persistence
{
    public interface IDataKeyRepository
    {
        Task<IEnumerable<DataKey<SharedDataKeyType>>> GetAllSharedAsync();

        Task<DataKey<SharedDataKeyType>> GetSharedAsync(SharedDataKeyType type);

        Task<IEnumerable<DataKey<T>>> GetAllAsync<T>()  where T : Enum;

        Task<DataKey<T>> GetAsync<T>(T type)  where T : Enum;

        Task CreateKeyAsync<T>(DataKey<T> dataKey)  where T : Enum;
    }
}