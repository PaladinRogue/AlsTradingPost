using System.Threading.Tasks;

namespace PaladinRogue.Libray.Core.Domain.DataProtectors
{
    public interface IHashFactory
    {
        Task<HashSet> GenerateHashAsync<T>(T data, string salt = null);
    }
}