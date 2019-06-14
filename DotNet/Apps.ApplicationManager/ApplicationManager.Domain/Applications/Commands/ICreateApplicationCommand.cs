using ApplicationManager.Domain.Applications.Models;

namespace ApplicationManager.Domain.Applications.Commands
{
    public interface ICreateApplicationCommand
    {
        Application Execute(CreateApplicationDdto createApplicationDdto);
    }
}
