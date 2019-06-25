using ApplicationManager.Domain.AuthenticationServices;

namespace ApplicationManager.Domain.Identities.RegisterPassword
{
    public interface IRegisterPasswordCommand
    {
        PasswordIdentity Execute(Identity identity, AuthenticationGrantTypePassword authenticationGrantTypePassword, RegisterPasswordCommandDdto registerPasswordCommandDdto);
    }
}