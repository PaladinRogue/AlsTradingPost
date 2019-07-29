using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReferenceData.Domain.Projections;

namespace ReferenceData.Domain.Persistence
{
    public interface IReferenceDataQueryRepository
    {
        Task<IEnumerable<ReferenceDataValueProjection>> GetAllAsync(string type);

        Task<ReferenceDataValueProjection> GetByIdAsync(Guid id);
    }
}