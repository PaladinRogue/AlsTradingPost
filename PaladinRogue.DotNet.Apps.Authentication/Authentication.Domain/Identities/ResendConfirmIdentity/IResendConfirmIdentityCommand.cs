using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.ResendConfirmIdentity
{
    public interface IResendConfirmIdentityCommand
    {
        Task ExecuteAsync(Identity identity);
    }
}