using System.Threading.Tasks;

namespace Common.Domain.DataProtectors
{
    public interface IMasterKeyProvider
    {
        Task<DataKey> GetAsync();
    }
}