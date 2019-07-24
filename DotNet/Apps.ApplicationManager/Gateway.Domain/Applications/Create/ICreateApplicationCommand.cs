using System.Threading.Tasks;

namespace Gateway.Domain.Applications.Create
{
    public interface ICreateApplicationCommand
    {
        Task<Application> ExecuteAsync(CreateApplicationDdto createApplicationDdto);
    }
}
