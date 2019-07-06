using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Users.Models;

namespace ApplicationManager.ApplicationServices.Users.CreateAdmin
{
    public interface ICreateAdminUserApplicationKernalService
    {
        Task CreateAsync(CreateUserAdto createUserAdto);
    }
}