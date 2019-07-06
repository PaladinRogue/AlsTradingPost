using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.ConfirmIdentity
{
    public interface IConfirmIdentityCommand
    {
        Task ExecuteAsync(Identity identity,
            ConfirmIdentityCommandDdto confirmIdentityCommandDdto);
    }
}