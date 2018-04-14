using AlsTradingPost.Domain.AdminDomain.Models;
using Common.Domain.Services;

namespace AlsTradingPost.Domain.AdminDomain.Interfaces
{
    public interface IAdminQueryService : IQueryService<AdminProjection>, ISummaryQueryService<AdminSummaryProjection>
    {
    }
}
