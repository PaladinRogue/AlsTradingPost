using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.Login.Password
{
    public interface IPasswordLoginCommand
    {
        Task<Identity> ExecuteAsync(PasswordLoginCommandDdto passwordLoginCommandDdto);
    }
}