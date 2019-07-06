using System.Threading.Tasks;

namespace ApplicationManager.Domain.Applications.Change
{
    public interface IChangeApplicationCommand
    {
        Task ExecuteAsync(Application application,
            ChangeApplicationDdto changeApplicationDdto);
    }
}
