using System.Threading.Tasks;

namespace PaladinRogue.Libray.Vault.Domain.Applications.AddDataKey
{
    public interface IAddApplicationDataKeyCommand
    {
        Task<ApplicationDataKey> ExecuteAsync(Application application, AddApplicationDataKeyCommandDdto addApplicationDataKeyCommandDdto);
    }
}