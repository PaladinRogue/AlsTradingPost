using System.Threading.Tasks;

namespace Common.Domain.DataProtectors
{
    public interface IHashFactory
    {
        Task<HashSet> GenerateHashAsync<T>(T data, string salt = null);
    }
}