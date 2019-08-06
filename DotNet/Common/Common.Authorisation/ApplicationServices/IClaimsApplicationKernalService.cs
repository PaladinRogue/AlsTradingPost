using System.Threading.Tasks;

namespace Common.Authorisation.ApplicationServices
{
    public interface IClaimsApplicationKernalService
    {
        Task AddAsync(AddClaimAdto addClaimAdto);
    }
}