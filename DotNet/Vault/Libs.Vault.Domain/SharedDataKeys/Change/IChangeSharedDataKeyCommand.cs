using System.Threading.Tasks;

namespace PaladinRogue.Libray.Vault.Domain.SharedDataKeys.Change
{
    public interface IChangeSharedDataKeyCommand
    {
        Task ExecuteAsync(SharedDataKey sharedDataKey, ChangeSharedDataKeyCommandDdto changeSharedDataKeyCommandDdto);
    }
}