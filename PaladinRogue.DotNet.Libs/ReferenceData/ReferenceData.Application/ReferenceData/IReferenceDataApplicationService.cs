using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Library.ReferenceData.Application.ReferenceData.Models;

namespace PaladinRogue.Library.ReferenceData.Application.ReferenceData
{
    public interface IReferenceDataApplicationService
    {
        Task<IEnumerable<ReferenceDataValueAdto>> GetAllAsync(GetAllReferenceDataAdto getAllReferenceDataAdto);

        Task<ReferenceDataValueAdto> GetAsync(GetReferenceDataAdto getReferenceDataAdto);
    }
}