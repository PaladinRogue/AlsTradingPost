using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Login.ClientCredential
{
    public interface IClientCredentialLoginCommand
    {
        Task ExecuteAsync(
            Identity identity);
    }
}