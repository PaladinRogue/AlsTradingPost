using System.Threading.Tasks;
using Authentication.Domain.AuthenticationServices;

namespace Authentication.Domain.Identities.RegisterPassword
{
    public interface IRegisterPasswordCommand
    {
        Task<PasswordIdentity> ExecuteAsync(Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            RegisterPasswordCommandDdto registerPasswordCommandDdto);
    }
}