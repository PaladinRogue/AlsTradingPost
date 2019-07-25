using System.Threading.Tasks;

namespace Authentication.Domain.Identities.Queries
{
    public interface IGetIdentityByForgotPasswordTokenQuery
    {
        Task<Identity> RunAsync(string token);
    }
}