using Microsoft.EntityFrameworkCore;

namespace PaladinRogue.Libray.ReferenceData.Persistence
{
    public interface IReferenceDataDbContext
    {
        DbSet<Domain.ReferenceDataType> ReferenceDataTypes { get; set; }
    }
}