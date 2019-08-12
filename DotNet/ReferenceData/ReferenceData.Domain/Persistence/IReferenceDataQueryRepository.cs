using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Library.ReferenceData.Domain.Projections;

namespace PaladinRogue.Library.ReferenceData.Domain.Persistence
{
    public interface IReferenceDataQueryRepository
    {
        Task<IEnumerable<ReferenceDataValueProjection>> GetAllAsync(string type);

        Task<ReferenceDataValueProjection> GetByCodeAsync(string type, string code);

        Task<ReferenceDataValueProjection> GetByIdAsync(Guid id);
    }
}