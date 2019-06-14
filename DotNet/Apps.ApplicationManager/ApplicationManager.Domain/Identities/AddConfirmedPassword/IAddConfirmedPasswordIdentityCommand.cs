using ApplicationManager.Domain.AuthenticationServices;

namespace ApplicationManager.Domain.Identities.AddConfirmedPassword
{
    public interface IAddConfirmedPasswordIdentityCommand
    {
        PasswordIdentity Execute(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            AddConfirmedPasswordIdentityDdto addConfirmedPasswordIdentityDdto);
    }
}