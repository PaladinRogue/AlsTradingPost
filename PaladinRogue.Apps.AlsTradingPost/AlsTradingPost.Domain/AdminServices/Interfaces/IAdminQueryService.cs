using AlsTradingPost.Domain.AdminServices.Models;
using Common.Domain.Interfaces;

namespace AlsTradingPost.Domain.AdminServices.Interfaces
{
    public interface IAdminQueryService : IQueryService<AdminProjection>, ISummaryQueryService<AdminSummaryProjection>
    {
    }
}
