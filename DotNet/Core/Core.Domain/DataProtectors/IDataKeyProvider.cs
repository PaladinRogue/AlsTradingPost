using System.Threading.Tasks;

namespace PaladinRogue.Libray.Core.Domain.DataProtectors
{
    public interface IDataKeyProvider
    {
        Task<DataKey> GetAsync(string name);
    }
}