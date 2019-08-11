using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Libray.ReferenceData.Application.ReferenceData.Models;

namespace PaladinRogue.Libray.ReferenceData.Application.ReferenceData
{
    public interface IReferenceDataApplicationService
    {
        Task<IEnumerable<ReferenceDataValueAdto>> GetAllAsync(GetAllReferenceDataAdto getAllReferenceDataAdto);

        Task<ReferenceDataValueAdto> GetAsync(GetReferenceDataAdto getReferenceDataAdto);
    }
}