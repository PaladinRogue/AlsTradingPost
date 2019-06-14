using ApplicationManager.Domain.Users.Models;

namespace ApplicationManager.Domain.Users.Commands
{
    public interface ICreateUserCommand
    {
        User Execute(CreateUserDdto createUserDdto);
    }
}