using System.Threading.Tasks;

namespace Authorisation.Application.ApplicationServices
{
    public interface IClaimsApplicationKernalService
    {
        Task AddAsync(AddClaimAdto addClaimAdto);
    }
}