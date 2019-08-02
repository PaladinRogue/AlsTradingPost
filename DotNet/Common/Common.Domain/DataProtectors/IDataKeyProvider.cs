using System.Threading.Tasks;

namespace Common.Domain.DataProtectors
{
    public interface IDataKeyProvider
    {
        Task<DataKey> GetAsync(string name);
    }
}