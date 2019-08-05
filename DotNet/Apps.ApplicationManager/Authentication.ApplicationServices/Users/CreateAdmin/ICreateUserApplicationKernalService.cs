using System.Threading.Tasks;
using Authentication.ApplicationServices.Users.Models;

namespace Authentication.ApplicationServices.Users.CreateAdmin
{
    public interface ICreateUserApplicationKernalService
    {
        Task CreateAsync(CreateUserAdto createUserAdto);
    }
}