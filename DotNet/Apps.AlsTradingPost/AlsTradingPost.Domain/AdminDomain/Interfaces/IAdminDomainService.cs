using AlsTradingPost.Domain.AdminDomain.Models;
using Common.Domain.Services.Domain;

namespace AlsTradingPost.Domain.AdminDomain.Interfaces
{
    public interface IAdminDomainService : IGetByIdService<AdminProjection>
    {
    }
}
