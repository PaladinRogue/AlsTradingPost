using AlsTradingPost.Domain.AdminServices.Models;
using Common.Domain.Services;

namespace AlsTradingPost.Domain.AdminServices.Interfaces
{
    public interface IAdminQueryService : IQueryService<AdminProjection>, ISummaryQueryService<AdminSummaryProjection>
    {
    }
}
