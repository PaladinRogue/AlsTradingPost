using System.Threading.Tasks;

namespace PaladinRogue.Library.Core.Domain.DataProtectors
{
    public interface IDataKeyProvider
    {
        Task<DataKey> GetAsync(string name);
    }
}