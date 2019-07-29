using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Repositories;
using ReferenceData.Domain.Persistence;
using ReferenceData.Domain.Projections;

namespace ReferenceData.Persistence
{
    public class ReferenceDataQueryRepository<TDbContext> : IReferenceDataQueryRepository where TDbContext : DbContext, IReferenceDataDbContext
    {
        private readonly TDbContext _referenceDataDbContext;

        public ReferenceDataQueryRepository(TDbContext referenceDataDbContext)
        {
            _referenceDataDbContext = referenceDataDbContext;
        }

        public async Task<IEnumerable<ReferenceDataValueProjection>> GetAllAsync(string type)
        {
            Domain.ReferenceDataType referenceDataType = await RepositoryHelper.GetSingleAsync(_referenceDataDbContext.ReferenceDataTypes, r => r.Type == type);

            return referenceDataType.ReferenceDataValues.Select(r => new ReferenceDataValueProjection
            {
                Id = r.Id,
                Code = r.Code
            });
        }

        public Task<ReferenceDataValueProjection> GetByIdAsync(Guid id)
        {
            return _referenceDataDbContext.Query<ReferenceDataValueProjection>()
                .FromSql($"SELECT TOP 1 * FROM [ReferenceDataValues] WHERE [Id] = {id}")
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
    }
}