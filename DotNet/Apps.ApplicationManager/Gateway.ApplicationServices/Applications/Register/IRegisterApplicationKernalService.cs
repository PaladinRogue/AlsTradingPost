using System.Threading.Tasks;

namespace Gateway.ApplicationServices.Applications.Register
{
    public interface IRegisterApplicationKernalService
    {
        Task RegisterAsync(RegisterApplicationAdto registerApplicationAdto);
    }
}

