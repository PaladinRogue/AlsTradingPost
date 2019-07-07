using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.Login.ClientCredential
{
    public interface IClientCredentialLoginCommand
    {
        Task ExecuteAsync(
            Identity identity);
    }
}