using System.Collections.Generic;
using System.Threading.Tasks;
using ReferenceData.Application.ReferenceData.Models;

namespace ReferenceData.Application.ReferenceData
{
    public interface IReferenceDataApplicationService
    {
        Task<IEnumerable<ReferenceDataValueAdto>> GetAllAsync(GetAllReferenceDataAdto getAllReferenceDataAdto);

        Task<ReferenceDataValueAdto> GetAsync(GetReferenceDataAdto getReferenceDataAdto);
    }
}