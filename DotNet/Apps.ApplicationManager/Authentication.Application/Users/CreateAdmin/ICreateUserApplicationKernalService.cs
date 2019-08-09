using System.Threading.Tasks;
using Authentication.Application.Users.Models;

namespace Authentication.Application.Users.CreateAdmin
{
    public interface ICreateUserApplicationKernalService
    {
        Task CreateAsync(CreateUserAdto createUserAdto);
    }
}