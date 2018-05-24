using AlsTradingPost.Domain.Models;
using Common.Domain.Services.Command;

namespace AlsTradingPost.Domain.AuditDomain.Interfaces
{
    public interface IAuditCommandService : ICreateCommandService<Audit>
    {
    }
}
