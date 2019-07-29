using Microsoft.EntityFrameworkCore;
using ReferenceData.Domain;

namespace ReferenceData.Persistence
{
    public interface IReferenceDataDbContext
    {
        DbSet<Domain.ReferenceDataType> ReferenceDataTypes { get; set; }
    }
}