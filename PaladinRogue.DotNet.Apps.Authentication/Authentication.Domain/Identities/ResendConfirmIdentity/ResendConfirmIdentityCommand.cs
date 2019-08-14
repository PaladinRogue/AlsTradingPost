using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.ResendConfirmIdentity
{
    public class ResendConfirmIdentityCommand : IResendConfirmIdentityCommand
    {
        public Task ExecuteAsync(Identity identity)
        {
            return identity.ResendConfirmIdentity();
        }
    }
}