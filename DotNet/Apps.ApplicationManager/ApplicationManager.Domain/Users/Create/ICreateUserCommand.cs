using System.Threading.Tasks;

namespace ApplicationManager.Domain.Users.Create
{
    public interface ICreateUserCommand
    {
        Task<User> ExecuteAsync(CreateUserCommandDdto createUserCommandDdto);
    }
}