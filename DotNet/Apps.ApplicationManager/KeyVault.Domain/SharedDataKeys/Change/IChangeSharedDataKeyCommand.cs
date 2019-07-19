using System.Threading.Tasks;

namespace KeyVault.Domain.SharedDataKeys.Change
{
    public interface IChangeSharedDataKeyCommand
    {
        Task ExecuteAsync(SharedDataKey sharedDataKey, ChangeSharedDataKeyCommandDdto changeSharedDataKeyCommandDdto);
    }
}