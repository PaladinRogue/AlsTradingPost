using Microsoft.EntityFrameworkCore;

namespace PaladinRogue.Library.ReferenceData.Persistence
{
    public interface IReferenceDataDbContext
    {
        DbSet<Domain.ReferenceDataType> ReferenceDataTypes { get; set; }
    }
}