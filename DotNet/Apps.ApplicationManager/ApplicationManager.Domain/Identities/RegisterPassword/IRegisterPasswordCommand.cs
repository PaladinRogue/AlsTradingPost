using System.Threading.Tasks;
using ApplicationManager.Domain.AuthenticationServices;

namespace ApplicationManager.Domain.Identities.RegisterPassword
{
    public interface IRegisterPasswordCommand
    {
        Task<PasswordIdentity> ExecuteAsync(Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            RegisterPasswordCommandDdto registerPasswordCommandDdto);
    }
}