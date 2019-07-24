using System.Threading.Tasks;

namespace Vault.Domain.SharedDataKeys.Create
{
    public interface ICreateSharedDataKeyCommand
    {
        Task<SharedDataKey> ExecuteAsync(CreateSharedDataKeyCommandDdto createSharedDataKeyCommandDdto);
    }
}