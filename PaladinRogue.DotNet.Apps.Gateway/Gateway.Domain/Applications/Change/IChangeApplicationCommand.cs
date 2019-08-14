using System.Threading.Tasks;

namespace PaladinRogue.Gateway.Domain.Applications.Change
{
    public interface IChangeApplicationCommand
    {
        Task ExecuteAsync(Application application,
            ChangeApplicationDdto changeApplicationDdto);
    }
}
