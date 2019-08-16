using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.ConfirmIdentity
{
    public interface IConfirmIdentityCommand
    {
        Task ExecuteAsync(Identity identity,
            ConfirmIdentityCommandDdto confirmIdentityCommandDdto);
    }
}