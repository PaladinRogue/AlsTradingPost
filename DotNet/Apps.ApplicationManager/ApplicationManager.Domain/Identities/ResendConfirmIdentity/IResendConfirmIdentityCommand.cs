using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.ResendConfirmIdentity
{
    public interface IResendConfirmIdentityCommand
    {
        Task ExecuteAsync(Identity identity);
    }
}