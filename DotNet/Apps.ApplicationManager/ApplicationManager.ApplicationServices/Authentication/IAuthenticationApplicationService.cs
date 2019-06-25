using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Authentication.Models;
using Common.ApplicationServices.Authentication;

namespace ApplicationManager.ApplicationServices.Authentication
{
    public interface IAuthenticationApplicationService
    {
        Task<JwtAdto> Password(PasswordAdto passwordAdto);
    }
}