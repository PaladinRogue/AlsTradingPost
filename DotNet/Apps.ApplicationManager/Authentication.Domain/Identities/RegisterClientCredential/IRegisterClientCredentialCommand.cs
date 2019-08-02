using System.Threading.Tasks;
using Authentication.Domain.AuthenticationServices;

namespace Authentication.Domain.Identities.RegisterClientCredential
{
    public interface IRegisterClientCredentialCommand
    {
        Task ExecuteAsync(
            Identity identity,
            AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            RegisterClientCredentialCommandDdto registerClientCredentialCommandDdto);
    }
}