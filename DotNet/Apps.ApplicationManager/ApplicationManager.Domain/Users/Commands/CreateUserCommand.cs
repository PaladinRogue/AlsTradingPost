using ApplicationManager.Domain.Users.Models;

namespace ApplicationManager.Domain.Users.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        public User Execute(CreateUserDdto createUserDdto)
        {
            return User.Create(createUserDdto.Identity);
        }
    }
}