using System.Threading.Tasks;

namespace Gateway.Domain.Applications.Change
{
    public interface IChangeApplicationCommand
    {
        Task ExecuteAsync(Application application,
            ChangeApplicationDdto changeApplicationDdto);
    }
}
