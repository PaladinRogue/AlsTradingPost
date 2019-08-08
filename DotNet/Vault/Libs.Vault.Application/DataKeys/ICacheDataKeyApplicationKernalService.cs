using System.Threading.Tasks;

namespace Libs.Vault.Broker.ApplicationServices
{
    public interface ICacheDataKeyApplicationKernalService
    {
        Task CreateAndCacheAllAsync<T>();
    }
}