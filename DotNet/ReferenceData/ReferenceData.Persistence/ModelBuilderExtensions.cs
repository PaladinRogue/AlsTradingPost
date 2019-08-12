using Microsoft.EntityFrameworkCore;
using PaladinRogue.Library.ReferenceData.Domain;
using PaladinRogue.Library.ReferenceData.Domain.Projections;

namespace PaladinRogue.Library.ReferenceData.Persistence
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