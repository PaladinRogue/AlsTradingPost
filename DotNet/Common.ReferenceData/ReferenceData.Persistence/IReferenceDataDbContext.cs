using Microsoft.EntityFrameworkCore;

namespace ReferenceData.Persistence
{
    public interface IReferenceDataDbContext
    {
        DbSet<Domain.ReferenceDataType> ReferenceDataTypes { get; set; }
    }
}