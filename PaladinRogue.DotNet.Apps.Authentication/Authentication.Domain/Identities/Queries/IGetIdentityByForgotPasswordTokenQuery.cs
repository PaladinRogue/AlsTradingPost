using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.Queries
{
    public interface IGetIdentityByForgotPasswordTokenQuery
    {
        Task<Identity> RunAsync(string token);
    }
}