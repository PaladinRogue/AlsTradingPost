using AlsTradingPost.Domain.AdminDomain.Models;
using Common.Domain.Services.Interfaces;

namespace AlsTradingPost.Domain.AdminDomain.Interfaces
{
    public interface IAdminQueryService : IGetByIdService<AdminProjection>
    {
    }
}
