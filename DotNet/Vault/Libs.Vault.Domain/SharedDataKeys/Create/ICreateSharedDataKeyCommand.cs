using System.Threading.Tasks;

namespace PaladinRogue.Library.Vault.Domain.SharedDataKeys.Create
{
    public interface ICreateSharedDataKeyCommand
    {
        Task<SharedDataKey> ExecuteAsync(CreateSharedDataKeyCommandDdto createSharedDataKeyCommandDdto);
    }
}