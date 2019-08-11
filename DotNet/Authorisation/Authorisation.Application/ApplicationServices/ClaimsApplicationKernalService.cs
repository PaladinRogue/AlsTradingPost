using System.Threading.Tasks;
using PaladinRogue.Authentication.Messages;
using PaladinRogue.Libray.Messaging.Common.Messages;

namespace PaladinRogue.Libray.Authorisation.Application.ApplicationServices
{
    public class ClaimsApplicationKernalService : IClaimsApplicationKernalService
    {
        public Task AddAsync(AddClaimAdto addClaimAdto)
        {
            return Message.SendAsync(AddAuthorisationClaimMessage.Create(addClaimAdto.IdentityId, addClaimAdto.Type, addClaimAdto.Value));
        }
    }
}