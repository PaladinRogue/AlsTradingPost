using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.ChangeClaim
{
    public interface IChangeIdentityClaimCommand
    {
        Task ExecuteAsync(Identity identity,
            ChangeIdentityClaimCommandDdto changeIdentityClaimCommandDdto);
    }
}