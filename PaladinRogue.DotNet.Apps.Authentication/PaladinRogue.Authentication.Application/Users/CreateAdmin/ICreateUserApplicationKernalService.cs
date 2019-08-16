using System.Threading.Tasks;
using PaladinRogue.Authentication.Application.Users.Models;

namespace PaladinRogue.Authentication.Application.Users.CreateAdmin
{
    public interface ICreateUserApplicationKernalService
    {
        Task CreateAsync(CreateUserAdto createUserAdto);
    }
}