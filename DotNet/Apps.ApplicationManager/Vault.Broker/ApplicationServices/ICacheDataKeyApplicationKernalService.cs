using System.Threading.Tasks;

namespace Vault.Broker.ApplicationServices
{
    public interface ICacheDataKeyApplicationKernalService
    {
        Task CreateAndCacheAllAsync<T>();
    }
}