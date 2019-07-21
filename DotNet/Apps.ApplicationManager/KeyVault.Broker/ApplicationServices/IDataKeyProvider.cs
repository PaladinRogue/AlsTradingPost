using System;
using System.Threading.Tasks;
using KeyVault.Broker.Domain;

namespace KeyVault.Broker.ApplicationServices
{
    public interface IDataKeyProvider
    {
        Task<DataKey<T>> GetAsync<T>(T type) where T : struct, Enum;
    }
}