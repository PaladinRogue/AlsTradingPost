using System.Threading.Tasks;
using Messaging.Messages;
using Messaging.Setup.Infrastructure;

namespace Authorisation.Application.ApplicationServices
{
    public class ClaimsApplicationKernalService : IClaimsApplicationKernalService
    {
        public Task AddAsync(AddClaimAdto addClaimAdto)
        {
            return Message.SendAsync(AddAuthorisationClaimMessage.Create(addClaimAdto.IdentityId, addClaimAdto.Type, addClaimAdto.Value));
        }
    }
}