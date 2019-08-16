using System.Threading.Tasks;
using PaladinRogue.Authentication.Domain.AuthenticationServices;

namespace PaladinRogue.Authentication.Domain.Identities.RegisterClientCredential
{
    public interface IRegisterClientCredentialCommand
    {
        Task ExecuteAsync(
            Identity identity,
            AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            RegisterClientCredentialCommandDdto registerClientCredentialCommandDdto);
    }
}