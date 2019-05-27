using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.AuthenticationIdentities;

namespace ApplicationManager.Domain.Identities
{
    public interface ICreatePasswordIdentityCommand
    {
        void Execute(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            CreatePasswordIdentityDdto createPasswordIdentityDdto);
    }
}