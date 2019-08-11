using System.Threading.Tasks;

namespace PaladinRogue.Libray.Core.Domain.DataProtectors
{
    public interface IDataProtector
    {
        Task<string> ProtectAsync<T>(T data, string keyName);

        Task<T> UnprotectAsync<T>(string data, string keyName);
    }
}