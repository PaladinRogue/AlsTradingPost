using ApplicationManager.ApplicationServices.Users.Models;

namespace ApplicationManager.ApplicationServices.Users.CreateAdmin
{
    public interface ICreateAdminUserApplicationKernalService
    {
        void Create(CreateUserAdto createUserAdto);
    }
}