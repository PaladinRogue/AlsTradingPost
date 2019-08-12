using System.Threading.Tasks;

namespace PaladinRogue.Library.Vault.Application.DataKeys
{
    public interface ICacheDataKeyApplicationKernalService
    {
        Task CreateAndCacheAllAsync<T>();
    }
}