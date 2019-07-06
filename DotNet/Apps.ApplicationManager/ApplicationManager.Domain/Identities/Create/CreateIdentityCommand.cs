using System.Threading.Tasks;

namespace ApplicationManager.Domain.Identities.Create
{
    public class CreateIdentityCommand : ICreateIdentityCommand
    {
        public Task<Identity> ExecuteAsync()
        {
            return Task.FromResult(Identity.Create());
        }
    }
}
