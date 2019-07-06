using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IGetIdentityByForgotPasswordTokenQuery
    {
        Task<Identity> RunAsync(string token);
    }
}