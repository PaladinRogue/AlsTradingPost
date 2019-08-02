using System.Threading.Tasks;

namespace Vault.Domain.SharedDataKeys.Change
{
    public interface IChangeSharedDataKeyCommand
    {
        Task ExecuteAsync(SharedDataKey sharedDataKey, ChangeSharedDataKeyCommandDdto changeSharedDataKeyCommandDdto);
    }
}