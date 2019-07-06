using System.Threading.Tasks;

namespace ApplicationManager.Domain.Applications.Create
{
    public interface ICreateApplicationCommand
    {
        Task<Application> ExecuteAsync(CreateApplicationDdto createApplicationDdto);
    }
}
