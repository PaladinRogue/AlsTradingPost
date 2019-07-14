using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.AddClaim
{
    public interface IAddIdentityClaimCommand
    {
        Task ExecuteAsync(
            Identity identity,
            AddIdentityClaimCommandDdto addIdentityClaimCommandDdto);
    }
}