using System.Threading.Tasks;

namespace Gateway.Application.Applications.Register
{
    public interface IRegisterApplicationKernalService
    {
        Task RegisterAsync(RegisterApplicationAdto registerApplicationAdto);
    }
}

