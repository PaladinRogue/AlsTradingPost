using System.Threading.Tasks;

namespace PaladinRogue.Libray.Vault.Domain.SharedDataKeys.Create
{
    public interface ICreateSharedDataKeyCommand
    {
        Task<SharedDataKey> ExecuteAsync(CreateSharedDataKeyCommandDdto createSharedDataKeyCommandDdto);
    }
}