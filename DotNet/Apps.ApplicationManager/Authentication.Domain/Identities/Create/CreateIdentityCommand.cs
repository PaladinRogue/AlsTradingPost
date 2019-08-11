using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Domain.Identities.Create
{
    public class CreateIdentityCommand : ICreateIdentityCommand
    {
        public Task<Identity> ExecuteAsync()
        {
            return Task.FromResult(Identity.Create());
        }
    }
}
