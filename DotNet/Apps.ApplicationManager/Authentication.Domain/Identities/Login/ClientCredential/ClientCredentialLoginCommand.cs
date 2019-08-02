using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Login.ClientCredential
{
    public class ClientCredentialLoginCommand : IClientCredentialLoginCommand
    {
        public Task ExecuteAsync(Identity identity)
        {
            identity.Login();

            return Task.CompletedTask;
        }
    }
}