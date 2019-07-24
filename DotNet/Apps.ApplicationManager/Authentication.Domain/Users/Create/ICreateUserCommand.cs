using System.Threading.Tasks;

namespace Authentication.Domain.Users.Create
{
    public interface ICreateUserCommand
    {
        Task<User> ExecuteAsync(CreateUserCommandDdto createUserCommandDdto);
    }
}