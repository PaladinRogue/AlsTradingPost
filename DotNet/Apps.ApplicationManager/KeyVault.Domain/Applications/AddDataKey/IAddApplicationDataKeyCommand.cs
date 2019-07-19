using System.Threading.Tasks;

namespace KeyVault.Domain.Applications.AddDataKey
{
    public interface IAddApplicationDataKeyCommand
    {
        Task<ApplicationDataKey> ExecuteAsync(Application application, AddApplicationDataKeyCommandDdto addApplicationDataKeyCommandDdto);
    }
}