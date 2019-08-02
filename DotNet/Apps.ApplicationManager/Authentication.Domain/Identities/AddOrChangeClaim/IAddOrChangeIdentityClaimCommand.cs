using System.Threading.Tasks;

namespace Authentication.Domain.Identities.AddOrChangeClaim
{
    public interface IAddOrChangeIdentityClaimCommand
    {
        Task ExecuteAsync(
            Identity identity,
            AddOrChangeIdentityClaimCommandDdto addOrChangeIdentityClaimCommandDdto);
    }
}