using System.Threading.Tasks;

namespace ReverseProxy.Domain.Applications.Create
{
    public interface ICreateApplicationCommand
    {
        Task<Application> ExecuteAsync(CreateApplicationDdto createApplicationDdto);
    }
}
