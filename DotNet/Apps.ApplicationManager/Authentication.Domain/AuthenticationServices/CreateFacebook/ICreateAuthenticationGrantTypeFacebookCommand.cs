using System.Threading.Tasks;

namespace Authentication.Domain.AuthenticationServices.CreateFacebook
{
    public interface ICreateAuthenticationGrantTypeFacebookCommand
    {
        Task<AuthenticationGrantTypeFacebook> ExecuteAsync(CreateAuthenticationGrantTypeFacebookDdto createAuthenticationGrantTypeFacebookDdto);
    }
}