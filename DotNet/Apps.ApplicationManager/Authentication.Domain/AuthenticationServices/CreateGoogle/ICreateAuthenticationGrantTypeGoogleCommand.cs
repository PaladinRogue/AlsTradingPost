using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.AuthenticationServices.CreateGoogle
{
    public interface ICreateAuthenticationGrantTypeGoogleCommand
    {
        Task<AuthenticationGrantTypeGoogle> ExecuteAsync(CreateAuthenticationGrantTypeGoogleDdto createAuthenticationGrantTypeGoogleDdto);
    }
}