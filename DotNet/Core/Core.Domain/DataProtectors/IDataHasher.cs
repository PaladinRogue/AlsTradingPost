using System.Threading.Tasks;

namespace PaladinRogue.Library.Core.Domain.DataProtectors
{
    public interface IDataHasher
    {
        Task<HashSet> StaticHashAsync(string data, string saltName);

        Task<HashSet> HashAsync(string data, string salt = null);
    }
}