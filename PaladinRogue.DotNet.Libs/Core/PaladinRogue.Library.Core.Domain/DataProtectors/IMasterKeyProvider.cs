using System.Threading.Tasks;

namespace PaladinRogue.Library.Core.Domain.DataProtectors
{
    public interface IMasterKeyProvider
    {
        Task<DataKey> GetAsync();
    }
}