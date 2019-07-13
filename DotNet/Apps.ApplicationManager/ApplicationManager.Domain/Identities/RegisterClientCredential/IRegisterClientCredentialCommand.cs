using System.Threading.Tasks;
using ApplicationManager.Domain.AuthenticationServices;

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