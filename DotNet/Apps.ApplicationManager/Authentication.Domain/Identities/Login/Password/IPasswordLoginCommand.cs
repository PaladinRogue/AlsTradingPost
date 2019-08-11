using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.Login.Password
{
    public interface IPasswordLoginCommand
    {
        Task<Identity> ExecuteAsync(PasswordLoginCommandDdto passwordLoginCommandDdto);
    }
}