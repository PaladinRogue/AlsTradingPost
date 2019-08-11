using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaladinRogue.Libray.Persistence.EntityFramework.Repositories;
using PaladinRogue.Libray.ReferenceData.Domain.Persistence;
using PaladinRogue.Libray.ReferenceData.Domain.Projections;

namespace PaladinRogue.Libray.ReferenceData.Persistence
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

        public Task<ReferenceDataValueProjection> GetByCodeAsync(string type, string code)
        {
            return _referenceDataDbContext.Query<ReferenceDataValueProjection>()
                .FromSql($@"SELECT TOP 1 *
                            FROM [authentication].[ReferenceDataValues] v
                            INNER JOIN [authentication].[ReferenceDataTypes] t
                            ON v.[ReferenceDataTypeId] = t.[Id]
                            WHERE v.[Code] = {code} AND t.[Type] = {type}")
                .AsNoTracking()
                .SingleOrDefaultAsync();
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