using Microsoft.EntityFrameworkCore;
using ReferenceData.Domain;
using ReferenceData.Domain.Projections;

namespace ReferenceData.Persistence
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder UseReferenceData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReferenceDataType>()
                .ToTable("ReferenceDataTypes")
                .HasAlternateKey(r => new { r.Type });


            modelBuilder.Entity<ReferenceDataValue>()
                .ToTable("ReferenceDataValues")
                .HasAlternateKey(r => new { r.Code, r.ReferenceDataTypeId });


            modelBuilder.Query<ReferenceDataValueProjection>()
                .ToView("ReferenceDataValueProjections");

            return modelBuilder;
        }
    }
}