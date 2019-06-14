using ApplicationManager.Domain.Applications.Models;

namespace ApplicationManager.Domain.Applications.Commands
{
    public interface IChangeApplicationCommand
    {
        void Execute(Application application, ChangeApplicationDdto changeApplicationDdto);
    }
}
