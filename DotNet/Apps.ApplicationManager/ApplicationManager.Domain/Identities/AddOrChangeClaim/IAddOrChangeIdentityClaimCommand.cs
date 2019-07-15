using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.AddOrChangeClaim
{
    public interface IAddOrChangeIdentityClaimCommand
    {
        Task ExecuteAsync(
            Identity identity,
            AddOrChangeIdentityClaimCommandDdto addOrChangeIdentityClaimCommandDdto);
    }
}