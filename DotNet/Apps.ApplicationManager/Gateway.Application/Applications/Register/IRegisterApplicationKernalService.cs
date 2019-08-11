using System.Threading.Tasks;

namespace PaladinRogue.Gateway.Application.Applications.Register
{
    public interface IRegisterApplicationKernalService
    {
        Task RegisterAsync(RegisterApplicationAdto registerApplicationAdto);
    }
}

