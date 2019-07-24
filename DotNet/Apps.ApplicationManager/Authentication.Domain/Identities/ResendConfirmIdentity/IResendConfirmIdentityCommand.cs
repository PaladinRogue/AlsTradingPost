using System.Threading.Tasks;

namespace Authentication.Domain.Identities.ResendConfirmIdentity
{
    public interface IResendConfirmIdentityCommand
    {
        Task ExecuteAsync(Identity identity);
    }
}