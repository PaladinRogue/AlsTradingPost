using System.Threading.Tasks;

namespace ReverseProxy.ApplicationServices.Applications
{
    public interface IApplicationKernalService
    {
        Task<ApplicationAdto> GetByNameAsync(string applicationName);
    }
}