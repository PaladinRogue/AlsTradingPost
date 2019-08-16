using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.Login.ClientCredential
{
    public interface IClientCredentialLoginCommand
    {
        Task ExecuteAsync(
            Identity identity);
    }
}