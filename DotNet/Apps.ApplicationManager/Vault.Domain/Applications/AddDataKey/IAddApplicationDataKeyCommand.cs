using System.Threading.Tasks;

namespace Vault.Domain.Applications.AddDataKey
{
    public interface IAddApplicationDataKeyCommand
    {
        Task<ApplicationDataKey> ExecuteAsync(Application application, AddApplicationDataKeyCommandDdto addApplicationDataKeyCommandDdto);
    }
}