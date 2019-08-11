using System.Threading.Tasks;

namespace PaladinRogue.Libray.Authorisation.Application.ApplicationServices
{
    public interface IClaimsApplicationKernalService
    {
        Task AddAsync(AddClaimAdto addClaimAdto);
    }
}