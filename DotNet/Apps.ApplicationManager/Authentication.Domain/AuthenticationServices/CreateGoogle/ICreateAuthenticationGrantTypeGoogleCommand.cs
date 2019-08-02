using System.Threading.Tasks;

namespace Authentication.Domain.AuthenticationServices.CreateGoogle
{
    public interface ICreateAuthenticationGrantTypeGoogleCommand
    {
        Task<AuthenticationGrantTypeGoogle> ExecuteAsync(CreateAuthenticationGrantTypeGoogleDdto createAuthenticationGrantTypeGoogleDdto);
    }
}