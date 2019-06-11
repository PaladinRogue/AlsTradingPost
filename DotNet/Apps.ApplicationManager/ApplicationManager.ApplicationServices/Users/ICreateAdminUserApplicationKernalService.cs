using ApplicationManager.ApplicationServices.Users.Models;

namespace ApplicationManager.ApplicationServices.Users
{
    public interface ICreateAdminUserApplicationKernalService
    {
        void Create(CreateUserAdto createUserAdto);
    }
}