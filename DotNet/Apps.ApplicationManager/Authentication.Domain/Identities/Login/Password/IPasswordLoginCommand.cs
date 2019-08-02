using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Login.Password
{
    public interface IPasswordLoginCommand
    {
        Task<Identity> ExecuteAsync(PasswordLoginCommandDdto passwordLoginCommandDdto);
    }
}