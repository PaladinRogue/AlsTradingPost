using System.Threading.Tasks;

namespace ApplicationManager.ApplicationServices.Applications.Register
{
    public interface IRegisterApplicationKernalService
    {
        Task RegisterAsync(RegisterApplicationAdto registerApplicationAdto);
    }
}

