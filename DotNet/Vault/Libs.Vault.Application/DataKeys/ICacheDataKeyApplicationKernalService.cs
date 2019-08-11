using System.Threading.Tasks;

namespace PaladinRogue.Libray.Vault.Application.DataKeys
{
    public interface ICacheDataKeyApplicationKernalService
    {
        Task CreateAndCacheAllAsync<T>();
    }
}