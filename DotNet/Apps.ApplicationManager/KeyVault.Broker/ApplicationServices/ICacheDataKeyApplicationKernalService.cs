using System.Threading.Tasks;

namespace KeyVault.Broker.ApplicationServices
{
    public interface ICacheDataKeyApplicationKernalService
    {
        Task CreateAndCacheAllAsync<T>();
    }
}