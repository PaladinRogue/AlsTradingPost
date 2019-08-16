using System.Threading.Tasks;

namespace PaladinRogue.Gateway.Domain.Applications.Create
{
    public interface ICreateApplicationCommand
    {
        Task<Application> ExecuteAsync(CreateApplicationDdto createApplicationDdto);
    }
}
