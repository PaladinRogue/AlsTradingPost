using System.Threading.Tasks;

namespace ReverseProxy.ApplicationServices.Applications.Register
{
    public interface IRegisterApplicationKernalService
    {
        Task RegisterAsync(RegisterApplicationAdto registerApplicationAdto);
    }
}

