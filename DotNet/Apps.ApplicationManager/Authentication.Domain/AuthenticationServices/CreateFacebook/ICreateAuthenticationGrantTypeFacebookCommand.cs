using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.AuthenticationServices.CreateFacebook
{
    public interface ICreateAuthenticationGrantTypeFacebookCommand
    {
        Task<AuthenticationGrantTypeFacebook> ExecuteAsync(CreateAuthenticationGrantTypeFacebookDdto createAuthenticationGrantTypeFacebookDdto);
    }
}