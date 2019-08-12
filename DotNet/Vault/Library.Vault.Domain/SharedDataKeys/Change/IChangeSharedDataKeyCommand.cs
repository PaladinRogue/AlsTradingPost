using System.Threading.Tasks;

namespace PaladinRogue.Library.Vault.Domain.SharedDataKeys.Change
{
    public interface IChangeSharedDataKeyCommand
    {
        Task ExecuteAsync(SharedDataKey sharedDataKey, ChangeSharedDataKeyCommandDdto changeSharedDataKeyCommandDdto);
    }
}