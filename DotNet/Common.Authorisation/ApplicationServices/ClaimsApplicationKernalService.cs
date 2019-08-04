using System.Threading.Tasks;
using Common.Messaging.Infrastructure;
using Common.Messaging.Messages;

namespace Common.Authorisation.ApplicationServices
{
    public class ClaimsApplicationKernalService : IClaimsApplicationKernalService
    {
        public Task AddAsync(AddClaimAdto addClaimAdto)
        {
            return Message.SendAsync(AddAuthorisationClaimMessage.Create(addClaimAdto.IdentityId, addClaimAdto.Type, addClaimAdto.Value));
        }
    }
}