using System.Threading.Tasks;
using PaladinRogue.Authentication.Domain.AuthenticationServices;

namespace PaladinRogue.Authentication.Domain.Identities.RegisterPassword
{
    public interface IRegisterPasswordCommand
    {
        Task<PasswordIdentity> ExecuteAsync(Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            RegisterPasswordCommandDdto registerPasswordCommandDdto);
    }
}