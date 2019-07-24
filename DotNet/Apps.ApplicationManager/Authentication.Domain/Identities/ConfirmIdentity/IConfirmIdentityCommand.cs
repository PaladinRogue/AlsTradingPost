using System.Threading.Tasks;

namespace Authentication.Domain.Identities.ConfirmIdentity
{
    public interface IConfirmIdentityCommand
    {
        Task ExecuteAsync(Identity identity,
            ConfirmIdentityCommandDdto confirmIdentityCommandDdto);
    }
}