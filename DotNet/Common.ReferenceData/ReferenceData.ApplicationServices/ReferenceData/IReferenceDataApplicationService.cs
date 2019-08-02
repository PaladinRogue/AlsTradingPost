using System.Collections.Generic;
using System.Threading.Tasks;
using ReferenceData.ApplicationServices.ReferenceData.Models;

namespace ReferenceData.ApplicationServices.ReferenceData
{
    public interface IReferenceDataApplicationService
    {
        Task<IEnumerable<ReferenceDataValueAdto>> GetAllAsync(GetAllReferenceDataAdto getAllReferenceDataAdto);

        Task<ReferenceDataValueAdto> GetAsync(GetReferenceDataAdto getReferenceDataAdto);
    }
}