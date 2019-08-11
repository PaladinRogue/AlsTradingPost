using System.Threading.Tasks;

namespace PaladinRogue.Libray.Core.Domain.DataProtectors
{
    public interface IMasterKeyProvider
    {
        Task<DataKey> GetAsync();
    }
}