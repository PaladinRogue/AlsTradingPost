using System.Threading.Tasks;

namespace PaladinRogue.Library.Authorisation.Application.ApplicationServices
{
    public interface IClaimsApplicationKernalService
    {
        Task AddAsync(AddClaimAdto addClaimAdto);
    }
}