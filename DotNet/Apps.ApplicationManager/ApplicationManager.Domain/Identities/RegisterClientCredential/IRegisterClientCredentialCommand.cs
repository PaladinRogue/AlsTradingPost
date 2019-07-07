using System.Threading.Tasks;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.Login.ClientCredential;

namespace ApplicationManager.Domain.Identities.RegisterClientCredential
{
    public interface IRegisterClientCredentialCommand
    {
        Task ExecuteAsync(
            Identity identity,
            AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            RegisterClientCredentialCommandDdto registerClientCredentialCommandDdto);
    }
}