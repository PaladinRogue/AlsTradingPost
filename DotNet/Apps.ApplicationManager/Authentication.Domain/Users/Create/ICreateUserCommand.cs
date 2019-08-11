using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Users.Create
{
    public interface ICreateUserCommand
    {
        Task<User> ExecuteAsync(CreateUserCommandDdto createUserCommandDdto);
    }
}