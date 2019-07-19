using System.Threading.Tasks;

namespace KeyVault.Domain.SharedDataKeys.Create
{
    public interface ICreateSharedDataKeyCommand
    {
        Task<SharedDataKey> ExecuteAsync(CreateSharedDataKeyCommandDdto createSharedDataKeyCommandDdto);
    }
}