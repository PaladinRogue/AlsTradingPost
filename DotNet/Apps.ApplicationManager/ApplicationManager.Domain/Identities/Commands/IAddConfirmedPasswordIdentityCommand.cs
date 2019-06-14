using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.Models;

namespace ApplicationManager.Domain.Identities.Commands
{
    public interface IAddConfirmedPasswordIdentityCommand
    {
        PasswordIdentity Execute(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            AddConfirmedPasswordIdentityDdto addConfirmedPasswordIdentityDdto);
    }
}