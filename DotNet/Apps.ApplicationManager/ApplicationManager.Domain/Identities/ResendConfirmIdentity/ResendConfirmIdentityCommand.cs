using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.ResendConfirmIdentity
{
    public class ResendConfirmIdentityCommand : IResendConfirmIdentityCommand
    {
        public Task ExecuteAsync(Identity identity)
        {
            return identity.ResendConfirmIdentity();
        }
    }
}