using System.Threading.Tasks;

namespace ReverseProxy.Domain.Applications.Change
{
    public interface IChangeApplicationCommand
    {
        Task ExecuteAsync(Application application,
            ChangeApplicationDdto changeApplicationDdto);
    }
}
